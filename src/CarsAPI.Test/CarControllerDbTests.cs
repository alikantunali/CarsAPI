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
    public class CarControllerDbTests
    {
        [Fact]
        public async Task GetCarbyId_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            var id = 0;
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            carControllerDb.ModelState.AddModelError("id", "not valid");

            //ACT 

            var result = await carControllerDb.GetCarFromDB(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task AddCar_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var newCar = new Car()
            {
                Id = 3,
                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            carControllerDb.ModelState.AddModelError("id", "not required");

            //ACT 

            var result = await carControllerDb.AddCarToDB(newCar);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            //ASSERT

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        }
        [Fact]
        public async Task DeleteCar_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {

            //Arrange
            var id = 0;
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            carControllerDb.ModelState.AddModelError("id", "not in range");

            //ACT 

            var result = await carControllerDb.DeleteCarFromDb(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task DeleteCar_ReturnsOktResult_WhenModelStateIsValid()
        {
            var id = 0;

            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);

            //ACT 

            var result = await carControllerDb.DeleteCarFromDb(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task AddCar_ReturnsCar_WhenModelStateIsValid()
        {

            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            //carControllerDb.ModelState.AddModelError("id", "not required");

            //ACT 

            var result = await carControllerDb.AddCarToDB(newCar);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task UpdateCar_ReturnsBadRequest_WhenModelStateIsInvalid()
        {

            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
            //Arrange
            var repositoryMock = new Mock<IDbCarInfoRepository>();
            var carControllerDb = new CarControllerDb(repositoryMock.Object);
            //carControllerDb.ModelState.AddModelError("id", "not required");

            //ACT 

            var result = await carControllerDb.UpdateExistingCar(newCar);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);

        }
    }
}
