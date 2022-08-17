using Common.DbDataContext;
using Common.Entities;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Repositories.CarDbListService
{    
    public class DbCarInfoRepository : IDbCarInfoRepository
    {
        //DATA CONTEXT FROM DB
        private readonly CarDataContext _context;

        private readonly ILogger<DbCarInfoRepository> _logger;

        //CONSTRUCTOR
        public DbCarInfoRepository(CarDataContext context, ILogger<DbCarInfoRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {
            if (carId <= 0)
            {
                _logger.LogInformation($"given id {carId} is not in range ");
                throw new ArgumentOutOfRangeException(nameof(carId));
            }
            var dbCar = await _context.Cars.FindAsync(carId);
            if (dbCar != null)
            {
              return dbCar;
            }
            else 
            {
                _logger.LogInformation($"No cars Found with given Id.");
                throw new ArgumentNullException($"No cars Found with given Id ({carId}).");
            }                                              
        }

        public async Task<IEnumerable<Car?>> GetCarsAsync()
        {
            //.OrderByDescending(i => i.Id)
            var list = _context.Cars.ToList();
            if (list.Count != 0)
            {
                return list;
                
            }
            return null;
        }

        public async Task<Car?> AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Given car added to db.");
            return car;

        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            if (car.Id <= 0)
            {
                _logger.LogInformation($"given id: {car.Id} is invalid");
                throw new Exception("Id is invalid");
            }
            else
            {
                var dbCar = await _context.Cars.FindAsync(car.Id);

                if (dbCar != null)
                {

                    dbCar.BrandName = car.BrandName;
                    dbCar.ManufactureYear = car.ManufactureYear;
                    dbCar.Model = car.Model;

                    await _context.SaveChangesAsync();

                    return car;
                }
                _logger.LogInformation("No car exist with given Id");
                throw new Exception("No car exists with given Id.");
            }

        }
        public async Task<List<Car>> DeleteCarAsync(int carId)
        {
            if (carId <= 0)

            {
                _logger.LogInformation("Invalid Id.");
                throw new Exception("Invalid Id.");

            }
            else
            {
                var dbCar = await _context.Cars.FindAsync(carId);

                if (dbCar != null)
                {

                    _context.Cars.Remove(dbCar);
                    await _context.SaveChangesAsync();
                    return await _context.Cars.ToListAsync();

                }
                _logger.LogInformation("No car exist with given Id");
                throw new ArgumentNullException("No car exist with given Id");
            }
        }
    }
}
