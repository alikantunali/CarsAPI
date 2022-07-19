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
        private Mock<IDbCarInfoRepository> _repositoryMock;
        private CarControllerDb _carControllerDb;

        public CarControllerDbTests()
        {
            _repositoryMock = new Mock<IDbCarInfoRepository>();
            _carControllerDb = new CarControllerDb(_repositoryMock.Object);
        }
        [Fact]
        public async Task GetCarbyId_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            var id = 0;
            _carControllerDb.ModelState.AddModelError("id", "not valid");

            //ACT 

            var result = await _carControllerDb.GetCarFromDB(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<Car>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            _repositoryMock.Verify(x => x.GetCarByIdFromDbAsync(It.IsAny<int>()), Times.Never);

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
            _carControllerDb.ModelState.AddModelError("id", "not required");

            //ACT 

            var result = await _carControllerDb.AddCarToDB(newCar);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            //ASSERT

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            _repositoryMock.Verify(x => x.AddCarToDbAsync(It.IsAny<Car>()), Times.Never);

        }
        [Fact]
        public async Task DeleteCar_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {

            //Arrange
            var id = 0;
            _carControllerDb.ModelState.AddModelError("id", "not in range");

            //ACT 

            var result = await _carControllerDb.DeleteCarFromDb(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);            
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            _repositoryMock.Verify(x=>x.UpdateCarInDbAsync(It.IsAny<Car>()),Times.Never);

        }

        [Fact]
        public async Task DeleteCar_ReturnsOktResult_WhenModelStateIsValid()
        { 
            //Arrange
            var id = 0;
          
            //ACT 

            var result = await _carControllerDb.DeleteCarFromDb(id);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);

            _repositoryMock.Verify(r => r.DeleteCarFromDbAsync(id), Times.Once);
        }

        [Fact]
        public async Task AddCar_ReturnsCar_WhenModelStateIsValid()
        {
            //Arrange
            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
        

            //ACT 

            var result = await _carControllerDb.AddCarToDB(newCar);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            var okObjectResult= Assert.IsType<OkObjectResult>(actionResult.Result);
            

            
            Assert.IsType<OkObjectResult>(actionResult.Result);
            _repositoryMock.Verify(x => x.AddCarToDbAsync(newCar), Times.Once);

        }

        [Fact]
        public async Task UpdateCar_ReturnsOK_WhenModelStateIsvalid()
        {
            //Arrange
            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
  

            //ACT 

            var result = await _carControllerDb.UpdateExistingCar(newCar);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            _repositoryMock.Verify(x => x.UpdateCarInDbAsync(newCar), Times.Once);

        }

        [Fact]
        public async Task GetAllCars()
        {

            var newCar = new Car()
            {
                Id= 1,
                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
            IEnumerable<Car> carList = new List<Car>()
            {
                newCar
            }.AsEnumerable();
            //Arrange
            
                 
            _repositoryMock.SetReturnsDefault(carList);
            
            
            //ACT 

            var returnResult = _carControllerDb.GetCarsFromDb().Result;

            var ObjectResult = returnResult.Result;

            //ASSERT
           
            Assert.IsType<ActionResult<IEnumerable<Car>>>(returnResult);
            Assert.IsType<OkObjectResult>(ObjectResult);

        }
    }
}
