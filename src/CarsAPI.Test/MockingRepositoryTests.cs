using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test
{
    public class MockingRepositoryTests
    {
        [Fact]
        public async void GetCarbyId_Returns_SpecificCar()
        {
            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            repositoryMock
            .Setup(r => r.GetCarByIdFromDbAsync(3))
            .ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 });


            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            

            //Act

            var car = carControllerDb.GetCarFromDB(3);
                        
            //Assert

            repositoryMock.Verify(r => r.GetCarByIdFromDbAsync(3));
            Assert.IsType<Car>(car);
        }
    }
}
