using Moq;
using System;
using Microsoft.EntityFrameworkCore;
using Common.DbDataContext;
using Common.Repositories.CarDbListService;
using Microsoft.Extensions.Logging;
using Common.Entities;

namespace Common.Test
{
    public class DbCarInfoRepositoryTests

    {
        private Mock<DbSet<Car>> _dbSet;
        private Mock<CarDataContext> _dbContext;
        private DbCarInfoRepository _repository;
        private Mock<ILogger<DbCarInfoRepository>> _logger;
        private Car _car;
        public DbCarInfoRepositoryTests()
        {


            _dbContext = new Mock<CarDataContext>();
            
            _logger = new Mock<ILogger<DbCarInfoRepository>>();
            

            _repository = new DbCarInfoRepository(_dbContext.Object, _logger.Object);

            _car = new Car()
            {
                Id = 1,
                BrandName = "AKLM",
                Model = "QQQQ",
                ManufactureYear = "2022"
            };
        }
        
        [Fact]
        public void GetCarbyId_WithInvalidCarId_ThrowsArgumentOutOfRangeException()
        {
         
                     
           //ACT
           
            var result = _repository.GetCarByIdFromDbAsync(It.IsAny<int>());
           var x = result.Exception;
                        
           
            
           //ASSERT 

            _dbContext.Verify(v=>v.Cars.FindAsync(), Times.Never);
           Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'carId')", x.InnerException.Message);

        }

        [Fact]
        public void GetCarbyId_WithId_ReturnsCarType()
        {

            //ACT
            
            _dbContext.Setup(x => x.Cars.FindAsync(_car.Id)).Returns(new ValueTask<Car?>(_car));
            
            
            var result = _repository.GetCarByIdFromDbAsync(_car.Id);
            var returnType = result.Result;

            //ASSERT 


            _dbContext.Verify(v =>v.Cars.FindAsync(_car.Id), Times.Once);
            Assert.IsType<Car>(returnType);

        }

        [Fact]
        public void GetCarbyId_WithId_ThrowsException_WhenReturnedValueIsNULL()
        {

            //ASSERT
            _dbContext.Setup(x => x.Cars.FindAsync(_car.Id)).Returns(null);

            //ACT

            var result = _repository.GetCarByIdFromDbAsync(_car.Id);
            var statusCode = result.Status;
            var exception = result.Exception;

            //ASSERT 

            Assert.IsType<AggregateException>(exception);
            Assert.Equal("Faulted", statusCode.ToString());
            Assert.Equal($"Value cannot be null. (Parameter 'No cars Found with given Id ({_car.Id})')", exception.InnerException.Message);
        }

        [Fact]
        public async Task GetAllCars_Returns_Null_WhenDataSetIsEmpty()
        {

            //ASSERT
            _dbContext.Setup(x=>x.Cars== _dbSet.Object).Returns(true);
//            _dbContext.Setup(x => x.Cars.ToListAsync()).Returns(_dbSet);
            /////  TO BE CONTINUED 
            //ACT

            var result = _repository.GetCarsFromDbAsync();
            Assert.Null(result);
            var statusCode = result.Status;
            var exception = result.Exception;

            //ASSERT             
            Assert.IsType<AggregateException>(exception);
            Assert.Equal("Faulted", statusCode.ToString());
            Assert.Equal($"Value cannot be null. (Parameter 'source')", exception.InnerException.Message);
        }
    }
}
