using Common.DbDataContext;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test.Repositories
{
    //TENTITY ADDED FOR TESTING PURPOSES
    public class DbCarInfoTestRepository : IDbCarInfoRepository
    {
        private readonly CarDataContext _context;

        public DbCarInfoTestRepository(CarDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Common.Entities.Car?> AddCarToDbAsync(Car request)
        {
            await _context.Cars.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;

        }

        public Task<List<Common.Entities.Car>> DeleteCarFromDbAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<Common.Entities.Car?> GetCarByIdFromDbAsync(int carId)
        {
            if (carId <= 0)
            {
               
                throw new ArgumentOutOfRangeException(nameof(carId));
            }

            var dbCar = await _context.Cars.FindAsync(carId);
            if (dbCar != null)
            {
                return dbCar;
            }
           
            throw new ArgumentNullException($"given id ({carId}) should not be null");
        }

        public async Task<IEnumerable<Car>> GetCarsFromDbAsync()

            {
                return _context.Cars.OrderByDescending(i => i.Id).ToList();

            }


        public async Task<Common.Entities.Car> UpdateCarInDbAsync(Car request)
        {
            if (request.Id <= 0)
            {

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

                throw new Exception("No car exist with given Id");

            }
        }
    }
}
