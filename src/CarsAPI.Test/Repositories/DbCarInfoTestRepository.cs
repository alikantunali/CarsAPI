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
    public class DbCarInfoTestRepository : IDbCarInfoRepository
    {
        private readonly CarDataContext _context;

        public DbCarInfoTestRepository(CarDataContext context, ILogger<DbCarInfoRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public Task<Car?> AddCarToDbAsync(Car request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> DeleteCarFromDbAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<Car?> GetCarByIdFromDbAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Car>> GetCarsFromDbAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Car?> UpdateCarInDbAsync(Car request)
        {
            throw new NotImplementedException();
        }
    }
}
