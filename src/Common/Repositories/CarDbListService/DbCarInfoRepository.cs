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

        public async Task<Car> GetCarByIdFromDbAsync(int carId)
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
            _logger.LogInformation($"given id ({carId}) should not be null");
            throw new ArgumentNullException($"given id ({carId}) should not be null");

        }

        public async Task<IEnumerable<Car>> GetCarsFromDbAsync()
        {
            return await _context.Cars.OrderByDescending(i => i.Id).ToListAsync();

        }

        public async Task<Car?> AddCarToDbAsync(Car request)
        {
            await _context.Cars.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;

        }

        public async Task<Car?> UpdateCarInDbAsync(Car request)
        {
            if (request.Id <= 0)
            {
                _logger.LogInformation($"given id: {request.Id} is invalid");
                throw new Exception("Id is invalid");
            }
            else
            {

                var dbCar = await _context.Cars.FindAsync(request.Id);

                if (dbCar != null)
                {

                    dbCar.BrandName = request.BrandName;
                    dbCar.ManufactureYear = request.ManufactureYear;
                    dbCar.Model = request.Model;

                    await _context.SaveChangesAsync();

                    return request;
                }
                _logger.LogInformation("No car exist with given Id");
                throw new Exception("No car exist with given Id");

            }

        }
        public async Task<List<Car>> DeleteCarFromDbAsync(int carId)
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
