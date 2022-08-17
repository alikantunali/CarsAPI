using Moq;
using Common.DbDataContext;
using Common.Repositories.CarDbListService;
using Microsoft.Extensions.Logging;
using Common.Entities; 
using Microsoft.EntityFrameworkCore;

namespace Common.Test.RepositoryTests
{
    public class DbCarInfoRepositoryTests
    {
       // private List<Car> _list, _emptyList;
        private Car car;
        private List<Car> list,emptyList;
        private Mock<CarDataContext> dataContext;        
        private Mock<ILogger<DbCarInfoRepository>> logger;
        private DbCarInfoRepository repository;
       


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

        public DbCarInfoRepositoryTests()
        {
            car = new Car()
            {
                Id = 1,
                BrandName = "AKLM",
                Model = "QQQQ",
                ManufactureYear = "2022"
            };

            list = new List<Car>();
            

            emptyList = new List<Car>();

        }
        [Fact]
        public void GetCars_ById_ReturnsException_WhenContextIsNull()
        {
            logger = new Mock<ILogger<DbCarInfoRepository>>();
            try
            {
                repository = new DbCarInfoRepository(dataContext?.Object, logger.Object);               
            }
            catch (ArgumentNullException ex)
            {

                Assert.Equal("Value cannot be null. (Parameter 'context')", ex.Message);
            }
        }

        [Fact]
        public void GetCars_ById_ReturnsException_WhenLoggerIsNull()
        {

            dataContext = new Mock<CarDataContext>();
            try
            {
                repository = new DbCarInfoRepository(dataContext.Object, logger?.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("Value cannot be null. (Parameter 'logger')", ex.Message);
                
            }
        }

        [Fact]
        public void GetCarbyId_WithInvalidCarId_ThrowsArgumentOutOfRangeException()
        {
            //ARRAMGE 
            dataContext = new Mock<CarDataContext>();
            logger = new Mock<ILogger<DbCarInfoRepository>>();
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);

            //ACT

            var result = repository.GetCarByIdAsync(It.IsAny<int>());
            var exception = result.Exception;

            //ASSERT 

            dataContext.Verify(v => v.Cars.FindAsync(), Times.Never);
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'carId')", exception?.InnerException?.Message);

        }

        [Fact]
        public void GetCar_WithId_ReturnsCarType()
        {
            //ARRANGE
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(car.Id)).Returns(new ValueTask<Car?>(car));
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.GetCarByIdAsync(car.Id);
            var returnType = result.Result;

            //ASSERT 
            dataContext.Verify(v => v.Cars.FindAsync(car.Id), Times.Once);
            Assert.IsType<Car>(returnType);

        }

        [Fact]
        public void GetCarbyId_WithId_ThrowsException_WhenReturnedValueIsNULL()
        {
            //ASSERT
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(car.Id)).Returns(null);
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.GetCarByIdAsync(car.Id);
            var statusCode = result.Status;
            var exception = result.Exception;

            //ASSERT 

            Assert.IsType<AggregateException>(exception);
            Assert.Equal("Faulted", statusCode.ToString());
            Assert.Equal($"Value cannot be null. (Parameter 'No cars Found with given Id ({car.Id}).')", exception?.InnerException?.Message);
        }

        [Fact]
        public void GetCarsFromDbAsync_ReturnsCarList_WhenListNotNull()
        {
            //ARRANGE 
            list.Add(car);

            var mockDbSet = GetMockDbSet(list);                                   
            dataContext = new Mock<CarDataContext>();            
            dataContext.Setup(x => x.Cars).Returns(mockDbSet.Object);

            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.GetCarsAsync();
            var returnedResult=result.Result;

            //ASSERT 
            Assert.IsType<List<Car>>(returnedResult);
            Assert.Equal(1, returnedResult.Count());            
            dataContext.Verify(x => x.Cars, Times.Once);
        }

        [Fact]
        public void GetAllCarsFromDbAsync_ReturnsNULL_WhenListIsNull()
            
        {
            //ARRANGE 
            var dbset = GetMockDbSet(emptyList);
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars).Returns(dbset.Object);
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.GetCarsAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            //ASSERT 
            
            Assert.Null(result);
            dataContext.Verify(x => x.Cars, Times.Once);
            
        }
        [Fact]
        public void AddCarsToDb_AddsSavesCar_ReturnsGivenCar()
        {

            //ARRANGE 
            var mockDbSet = GetMockDbSet(emptyList);
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.SaveChangesAsync(default));
            dataContext.Setup(x => x.Cars).Returns(mockDbSet.Object);
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);

            var result = repository.AddCarAsync(car);

            var returnedValue = result.Result;

            //ASSERT 
            Assert.IsType<Car>(returnedValue);
            
            dataContext.Verify(x=>x.Cars, Times.Once);
            dataContext.Verify(x => x.Cars.AddAsync(car,default), Times.Once);
            dataContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public void DeleteCarFromDB_ThrowsException_WhenIdIsNotValid()
        {

            //ARRANGE             
            dataContext = new Mock<CarDataContext>();
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);

            var result = repository.DeleteCarAsync(It.IsAny<int>());


            var returnedValue = result.Exception;
            var message = returnedValue?.InnerException?.Message;

            //ASSERT 

            Assert.IsType<AggregateException>(returnedValue);
            Assert.Equal("Invalid Id.", message);

        }

        [Fact]
        public void DeleteCarFromDB_ReturnsCarList_WhenIdIsInRange()
        {

            //ARRANGE 
            Car car2 = new Car()
            {
                Id = 2,
                BrandName = "BBBB",
                Model = "WWWW",
                ManufactureYear = "2022"
            };
            list.Add(car2);

            var mockDbSet = GetMockDbSet(list);            
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(car.Id)).Returns(new ValueTask<Car?>(car));
           // dataContext.Setup(x => x.Cars).Returns(mockDbSet.Object);
            logger = new Mock<ILogger<DbCarInfoRepository>>();
            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);

            var result = repository.DeleteCarAsync(1);            
            //var returnedValue = result.Result;
            //var x = returnedValue.ToList();
            //ASSERT             
            Assert.IsType<Task<List<Car>>>(result);
            dataContext.Verify(x=>x.Cars.FindAsync(car.Id), Times.Once);
        }

        [Fact]
        public void DeleteCarFromDB_ThrowsException_WhenIdIsNotInRange()
        {

            //ARRANGE 
            var mockDbSet = GetMockDbSet(list);
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(car.Id)).Returns(new ValueTask<Car?>(car));           
            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.DeleteCarAsync(2);
            var exception = result.Exception;
            var message = exception?.InnerException?.Message;            
            //ASSERT
            //
            Assert.IsType<AggregateException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'No car exist with given Id')", message);
            dataContext.Verify(x => x.Cars.FindAsync(2), Times.Once);
            
        }

        [Fact]
        public void UpdateCar_ReturnsCarObject_WhenUpdateIsSuccessful()
        {
            //ARRANGE 

            
            Car updatedCar = new Car()
            {
                Id = 1,
                BrandName = "BBBB",
                Model = "WWWW",
                ManufactureYear = "2022"
            };
            list.Add(car);
            var mockDbSet = GetMockDbSet(list);
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars).Returns(mockDbSet.Object);
            dataContext.Setup(x => x.Cars.FindAsync(updatedCar.Id)).Returns(new ValueTask<Car?>(car));
            dataContext.Setup(x => x.SaveChangesAsync(default));
            dataContext.Setup(x => x.Cars).Returns(mockDbSet.Object);

            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);

            var result = repository.UpdateCarAsync(updatedCar).ConfigureAwait(true).GetAwaiter().GetResult();

            var resultCarModel = result.Model;
            

            //ASSERT 
            Assert.IsType<Car>(result);
            Assert.Equal("WWWW", resultCarModel);
            dataContext.Verify(x => x.Cars.FindAsync(updatedCar.Id), Times.Once);
            dataContext.Verify(x=>x.SaveChangesAsync(default), Times.Once);
            dataContext.Verify(x=>x.Cars, Times.Once);

        }

        [Fact]
        public void UpdateCar_ReturnsException_WhenNoCarExistsWithGivenId()
        {
            //ARRANGE 
            Car invalidCar = new Car()
            {
                Id = 2,
                BrandName = "asdrwe",
                Model = "typoui",
                ManufactureYear = "1999"
            };
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(invalidCar.Id));

            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.UpdateCarAsync(invalidCar);
            var exception = result?.Exception?.InnerException;

            //ASSERT             
            Assert.IsType<Exception>(exception);
            Assert.Equal("No car exists with given Id.", exception?.Message);
            dataContext.Verify(x => x.Cars.FindAsync(invalidCar.Id), Times.Once);

        }

        [Fact]
        public void UpdateCar_ReturnsException_WhenCarIdIsZero()
        {
            //ARRANGE 
            Car zeroCar = new Car()
            {
                Id = 0,
                BrandName = "zero",
                Model = "orez",
                ManufactureYear = "9999"
            };
            dataContext = new Mock<CarDataContext>();
            dataContext.Setup(x => x.Cars.FindAsync(zeroCar.Id));

            logger = new Mock<ILogger<DbCarInfoRepository>>();

            //ACT
            repository = new DbCarInfoRepository(dataContext.Object, logger.Object);
            var result = repository.UpdateCarAsync(zeroCar);
            var exception = result?.Exception?.InnerException;

            //ASSERT             
            Assert.IsType<Exception>(exception);
            Assert.Equal("Id is invalid", exception?.Message);
            dataContext.Verify(x => x.Cars.FindAsync(zeroCar.Id), Times.Never);

        }
    }


}
