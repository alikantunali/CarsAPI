using Moq;
using System;
using Microsoft.EntityFrameworkCore;
using Common.DbDataContext;
using Common.Repositories.CarDbListService;
using Microsoft.Extensions.Logging;
using Common.Entities;

namespace Common.Test.RepositoryTests
{
    public class DbCarInfoRepositoryTests2
    {
        private List<Car> _list, _emptyList;
        private Car _car;
        private Mock<CarDataContext> _dataContext;
        private DbCarInfoRepository _repository;
        private Mock<ILogger<DbCarInfoRepository>> _logger;
        private Mock<DbSet<Car>> _dbSet;


        private static Mock<DbSet<T>> GetMockDbSet<T>(List<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);

            return mockSet;
        }

        public DbCarInfoRepositoryTests2()
        {
            _car = new Car()
            {
                Id = 1,
                BrandName = "AKLM",
                Model = "QQQQ",
                ManufactureYear = "2022"
            };

            _list = new List<Car>();
            _list.Add(_car);

            _emptyList = new List<Car>();






            _logger = new Mock<ILogger<DbCarInfoRepository>>();

            //System.ArgumentNullException : Value cannot be null. (Parameter 'context')
        }
        [Fact]
        public void GetCars_ById_ReturnsException_WhenContextIsNull()
        {
            try
            {
                _repository = new DbCarInfoRepository(_dataContext.Object, _logger.Object);
            }
            catch (ArgumentNullException ex)
            {

                Assert.Equal("Value cannot be null. (Parameter 'context')", ex.Message);
            }
        }

        [Fact]
        public void GetCarsFromDbAsync_WhenList_IsNotNull()
        {
            //var mockDbSet = GetMockDbSet(_list);
            _dbSet = new Mock<DbSet<Car>>();


            _dataContext = new Mock<CarDataContext>();
            _dataContext.Setup(m => m.Cars).Returns(_dbSet.Object);

            _repository = new DbCarInfoRepository(_dataContext.Object, _logger.Object);
            _ = _repository.GetCarsFromDbAsync();

            _dbSet.Verify(m => m.OrderByDescending(i => i.Id), Times.Once);
            //_dataContext.Verify(m=>m.Cars.ToListAsync());
        }


    }


}
