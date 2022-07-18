using CarsAPI.Test.Repositories;
using Common.DbDataContext;
using Common.Entities;
using Common.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repositories.CarDbListService;
using CarsAPI.Controllers;

namespace CarsAPI.Test
{
    public class DbRepoTests
    {



        TestCar testObject = new TestCar()
        {
            Id=1,
            BrandName = "BMW",
            ManufactureYear = "2022",
            Model = "320I"
        };

        


        [Fact]
        public void Get_IndividualTestCarObjectPassed_ProperMethodCalled()
        {
            //Arrange                       

            var context = new Mock<CarDataContext>();
            var dbSetMock = new Mock<DbSet<TestCar>>();

            context.Setup(x => x.Set<TestCar>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(testObject);


            //ACT
            var repository = new DbCarInfoTestRepository(context.Object);
            
            _ = repository.GetCarByIdFromDbAsync(1);


            //ASSERT

            context.Verify(x => x.Set<TestCar>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));
        }

        [Fact]
        public void Load_CarsAll_ProperMethodCalled()
        {
            //Arrange
            //
            var testCar = new Car()
            {
                Id = 1,
                BrandName = "BMW",
                ManufactureYear = "2022",
                Model = "320I"
            };

            var interfaceMock = new Mock<IDbCarInfoRepository>()
                .Setup(x => x.UpdateCarInDbAsync(testCar))
                .Returns(Task.FromResult(testCar));

            var controllerMock = new Mock<CarControllerDb>(interfaceMock);

            var expected = testCar;
            var actual = controllerMock.Object.UpdateExistingCar;
                
            Assert.NotNull(actual);

        }

        [Fact]
        public void Add_TestCarObjectPassed_ProperMethodCalled()
        {
            //Arrange
            var testList = new List<TestCar>()
            {               
            };

            
         
            var dbSetMock = new Mock <DbSet<TestCar>>();            
            dbSetMock.Setup(d => d.Add(It.IsAny<TestCar>())).Callback<TestCar>((testObject) => testList.Add(testObject));

            var contextMock = new Mock<CarDataContext>();
            contextMock.Setup(x => x.Set<TestCar>()).Returns(dbSetMock.Object);            

            

            //ACT
            var repository = new DbCarInfoTestRepository(contextMock.Object);
            _ = repository.AddCarToDbAsync(testObject);


            //ASSERT

            contextMock.Verify(x => x.Set<TestCar>());
            dbSetMock.Verify(x => x.Add(It.Is<TestCar>(y=> y== testObject)));
        }

        [Fact]
        public void Update_TestCarObjectPassed_ProperMethodCalled()
        {
            //Arrange

            var context = new Mock<CarDataContext>();
           
            var dbSetMock = new Mock<DbSet<TestCar>>();

            context.Setup(x => x.Set<TestCar>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Update(It.Is<TestCar>(y => y == testObject)));


            //ACT
            var repository = new DbCarInfoTestRepository(context.Object);
            _ = repository.UpdateCarInDbAsync(testObject);


            //ASSERT

            context.Verify(x => x.Set<TestCar>());
            dbSetMock.Verify(x => x.Update(It.Is<TestCar>(y => y == testObject)));
        }

/*        [Fact]
        public async void Delete_IndividualTestCarObjectPassed_ProperMethodCalled()
        {
            //Arrange                       
            var listObject = new List<TestCar>()
            {
               testObject,
               new TestCar()
               {
                    Id=2,
                    BrandName = "MAZDA",
                    ManufactureYear = "2022",
                    Model = "XTYPE"
               }
            }
            .AsQueryable();

            var dbSetMock = new Mock<DbSet<Car>>();
            dbSetMock.As<IQueryable<TestCar>>().Setup(x => x.Provider).Returns(listObject.AsQueryable().Provider);
            dbSetMock.As<IQueryable<TestCar>>().Setup(x => x.Expression).Returns(listObject.AsQueryable().Expression);
            dbSetMock.As<IQueryable<TestCar>>().Setup(x => x.ElementType).Returns(listObject.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<TestCar>>().Setup(x => x.GetEnumerator()).Returns(listObject.AsQueryable().GetEnumerator());

            var context = new Mock<CarDataContext>();
            context.Setup(x => x.Cars).Returns(dbSetMock.Object);


            //context.Setup(x => x.Set<TestCar>()).Returns(dbSetMock.Object);
            //context.Setup(x=>x.Find(It.IsAny<
            //dbSetMock.Setup(x => x.Remove(It.IsAny<TestCar>())).Returns((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TestCar>)listObject);
            //dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(listObject.FirstOrDefault(y=>y.Id==listObject.));
            //dbSetMock.Setup(x => x.

            //ACT
            var repository = new DbCarInfoTestRepository(context.Object);
            var id = 2;
            await repository.DeleteCarFromDbAsync(id);
            
           

            //ASSERT
            dbSetMock.Verify(x=>x.Remove(It.IsAny<TestCar>()), Times.Once);
            context.Verify(m=>m.SaveChanges(), Times.Once);
            dbSetMock.Verify(x => x.Remove(It.IsAny<int>()));
        }
*/
        [Fact]
        public async void Delete_TestCarObjectPassed_ProperMethodCalled()

        {
            //ARRANGE 

            var context= new Mock<CarDataContext>();
            var logger = new Mock<ILogger<DbCarInfoRepository>>();
            var repository = new DbCarInfoRepository(context.Object, logger.Object)
            {
                
            };
            var listObjects = new List<Car>()
            {
               testObject,
               new Car()
               {
                    Id=2,
                    BrandName = "MAZDA",
                    ManufactureYear = "2022",
                    Model = "XTYPE"
               }
            };

            context
                .Setup(c=>c.Cars.Remove(It.IsAny<Car>()))
                .Callback<Car>((entity) => listObjects.Remove(entity));
            int idToDelete = 2;

            context
                .Setup(c => c.Cars.Find(idToDelete))
                .Returns(listObjects.Single(c => c.Id == idToDelete));

            //ACT

            //call delete method

            await repository.UpdateCarInDbAsync(testObject);

            // 1 object should be deleted. it should return remaining object.

            //ASSERT 

            Assert.Equal(1, listObjects.Count());

            context.Verify(c => c.Cars.Find(idToDelete), Times.Once);
            context.Verify(c=>c.Cars.Remove(It.IsAny<TestCar>()), Times.Once);
            context.Verify(c=>c.SaveChanges(), Times.Once);



        }
    }
}
