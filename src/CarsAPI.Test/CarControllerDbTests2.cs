using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace CarsAPI.Test
{
    public class CarControllerDbTests2
    {


        private Car TestCar = new Car()
        {
            Id = 1,
            BrandName = "xxx",
            ManufactureYear = "2022",
            Model = "yyy"
        };

        private Mock<IDbCarInfoRepository> _carRepository;
        private CarControllerDb _carControllerDb;

        public CarControllerDbTests2()
        {
            _carRepository = new Mock<IDbCarInfoRepository>();
            _carControllerDb = new CarControllerDb(_carRepository.Object);
        }
       
        

        [Fact]
        public async Task GetCars_Runs_AndCompletes()
        {
            //Arrange



            _carRepository
            .Setup(r => r.GetCarsFromDbAsync());            
            //Act

            var result = _carControllerDb.GetCarsFromDb().IsCompleted;

            //Assert

            _carRepository.Verify(r => r.GetCarsFromDbAsync(),Times.Once);
            
            Assert.True(result);
        }

        [Fact]
        public void GetCars_RunsOnce_ReturnsAList()
        {
            //Arrange
            var carList= new List<Car>();
            carList.Add(TestCar);

            _carRepository
            .Setup(r => r.GetCarsFromDbAsync())
            .ReturnsAsync(carList);          


            //Act

            var result = _carControllerDb.GetCarsFromDb() ;
            ActionResult<IEnumerable<Car>> returnedCar = result.Result;
            var actual = returnedCar.Result;


            //Assert

            _carRepository.Verify(r => r.GetCarsFromDbAsync(), Times.Once);

           // Assert.True(result);




        }
        [Fact]
        public void CarsReturn_Notnull()
        {
            //Arrange

            var returnedCar = TestCar;


            _carRepository
            .Setup(r => r.GetCarsFromDbAsync());

            //Act

            var result = _carControllerDb.GetCarsFromDb().Result;           
            //Assert

            Assert.NotNull(result);
            _carRepository.Verify(r => r.GetCarsFromDbAsync(), Times.Once);
            
            
        }


    }
}
