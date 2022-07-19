using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace CarsAPI.Test
{
    public class DbCarInfoRepositoryTests
    {
        public int carId { get; set; }

        public bool carExists { get; set; }

        Car TestCar = new Car()
        {
            Id = 1,
            BrandName = "xxx",
            ManufactureYear = "2022",
            Model = "yyy"
        };


        Mock<IDbCarInfoRepository> carRepository = new Mock<IDbCarInfoRepository>();
        
        [Fact]
        public async Task GetCars_Runs_AndCompletes()
        {
            //Arrange



            carRepository
            .Setup(r => r.GetCarsFromDbAsync());
            
            var carControllerDb = new CarControllerDb(carRepository.Object);

            //Act

            var result = carControllerDb.GetCarsFromDb().IsCompleted;

            //Assert

            carRepository.Verify(r => r.GetCarsFromDbAsync(),Times.Once);
            
            Assert.True(result);
        }

        [Fact]
        public void GetCars_RunsOnce_ReturnsAList()
        {
            //Arrange
            var carList= new List<Car>();
            carList.Add(TestCar);

            carRepository
            .Setup(r => r.GetCarsFromDbAsync())
            .ReturnsAsync(carList);


            var carControllerDb = new CarControllerDb(carRepository.Object);


            //Act

            var result = carControllerDb.GetCarsFromDb() ;
            ActionResult<IEnumerable<Car>> returnedCar = result.Result;
            var actual = returnedCar.Result;
            

            //Assert

            carRepository.Verify(r => r.GetCarsFromDbAsync(), Times.Once);

           // Assert.True(result);




        }
        [Fact]
        public void CarsReturn_Notnull()
        {
            //Arrange

            var returnedCar = TestCar;


            carRepository
            .Setup(r => r.GetCarsFromDbAsync());

            var carControllerDb = new CarControllerDb(carRepository.Object);

            //Act

            var result = carControllerDb.GetCarsFromDb().Result;           
            //Assert

            Assert.NotNull(result);
            carRepository.Verify(r => r.GetCarsFromDbAsync(), Times.Once);
            
            
        }


    }
}
