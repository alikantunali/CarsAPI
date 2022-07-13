using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test
{
    public class DbInfoMoqRepositoryTests
    {
        [Fact]
        public async void GetCars_Returns_TypeCar()
        {
            //Arrange
            var isTrue = true;
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            repositoryMock
            .Setup(r => r.GetCarsFromDbAsync());


            var carControllerDb = new CarControllerDb(repositoryMock.Object);


            //Act

            var result = carControllerDb.GetCarsFromDb().IsCompleted;
            
            //Assert

            repositoryMock.Verify(r => r.GetCarsFromDbAsync());

           
            Assert.Equal(isTrue, result);

        }
        [Fact]
        public async Task GetCarbyId_Returns_Value()
        {
            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            repositoryMock
            .Setup(r => r.GetCarByIdFromDbAsync(3))       
            .ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 });


            var carControllerDb = new CarControllerDb(repositoryMock.Object);


            //Act


            var result = carControllerDb.GetCarFromDB(3);
            //Assert           
            

           repositoryMock.Verify(r => r.GetCarByIdFromDbAsync(3));

            Assert.NotNull(result);
            

        }
       
    }
}
