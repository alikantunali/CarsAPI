using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Common.Test.FakeData;
using Microsoft.AspNetCore.Mvc;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test.Controllers
{
    public class CarControllerDbTests
    {
        private Mock<IDbCarInfoRepository> repositoryMock;
        private CarControllerDb carControllerDb;
        private Car testCar;
        public CarControllerDbTests()
        {
            testCar = new Car()
            {
                Id = 1,
                BrandName = "xxx",
                ManufactureYear = "2022",
                Model = "yyy"
            };
            repositoryMock = new Mock<IDbCarInfoRepository>();
            carControllerDb = new CarControllerDb(repositoryMock.Object);
        }

        [Fact]
        public void GetCars_Runs_AndCompletes()
        {
            //ARRANGE
            repositoryMock.Setup(r => r.GetCarsFromDbAsync());

            //ACT
            var result = carControllerDb.GetCarsFromDb().IsCompleted;

            //ASSERT
            repositoryMock.Verify(r => r.GetCarsFromDbAsync(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void GetCars_RunsOnce_ReturnsAList()
        {
            //ARRANGE
            var carList = new List<Car>();
            carList.Add(testCar);
            repositoryMock.Setup(r => r.GetCarsFromDbAsync()).ReturnsAsync(carList);

            //ACT
            var result = carControllerDb.GetCarsFromDb();
            ActionResult<IEnumerable<Car>> returnedCar = result.Result;
            var actual = returnedCar.Result;

            //ASSERT
            repositoryMock.Verify(r => r.GetCarsFromDbAsync(), Times.Once);
        }

        [Fact]
        public void GetCars_Returns_Notnull()
        {
            //ARRANGE
            var returnedCar = testCar;
            repositoryMock.Setup(r => r.GetCarsFromDbAsync());

            //ACT
            var result = carControllerDb.GetCarsFromDb().Result;
            
            //ASSERT
            Assert.NotNull(result);
            repositoryMock.Verify(r => r.GetCarsFromDbAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCarbyId_WithInvalidId_ReturnsBadRequest()
        {
            //ARRANGE
            var id = 0;
            carControllerDb.ModelState.AddModelError("id", "not valid");

            //ACT 
            var result = await carControllerDb.GetCarFromDB(id);
            var actionResult = Assert.IsType<ActionResult<Car>>(result);

            //ASSERT            
            Assert.IsType<BadRequestResult>(actionResult.Result);
            repositoryMock.Verify(x => x.GetCarByIdFromDbAsync(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task GetCarbyId_WithValidId_ReturnsOkCar()
        {
            //ARRANGE
            var id = 1;            
            repositoryMock.Setup(_ => _.GetCarByIdFromDbAsync(id)).ReturnsAsync(CarMockData.GetData().First());      

            //ACT 
            var result = await carControllerDb.GetCarFromDB(id);
            
            //ASSERT
            var actionResult = Assert.IsType<ActionResult<Car>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            repositoryMock.Verify(x => x.GetCarByIdFromDbAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetCarbyId_WithValidId_ReturnsNoContent()
        {
            //ARRANGE
            var outOfRangeId= CarMockData.GetData().Count +1;            
            carControllerDb.NoContent(); 

           //ACT 
           var result = await carControllerDb.GetCarFromDB(outOfRangeId);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<Car>>(result);
            Assert.IsType<NoContentResult>(actionResult.Result);
            repositoryMock.Verify(x => x.GetCarByIdFromDbAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            //ARRANGE
            repositoryMock.Setup(_ => _.GetCarsFromDbAsync()).ReturnsAsync(CarMockData.GetData());

            //ACT
            var result = carControllerDb.GetCarsFromDb();
            var actionResult = await result;
            var okResult = actionResult.Result;

            //ASSERT
            Assert.IsType<OkObjectResult>(okResult);
            repositoryMock.Verify(_ => _.GetCarsFromDbAsync(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllAsync_ShouldReturn204NoContentStatus()
        {
            //ARRANGE
            repositoryMock.Setup(_ => _.GetCarsFromDbAsync()).ReturnsAsync(CarMockNoData.GetEmptyCarList());

            //ACT
            var result = carControllerDb.GetCarsFromDb().Result;
            var actionResult = result.Result;
            var noContentResult = actionResult;

            //ASSERT
            Assert.IsType<NoContentResult>(noContentResult);
            repositoryMock.Verify(_ => _.GetCarsFromDbAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task AddCar_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            //ARRANGE
            var newCar = new Car()
            {
                Id = 3,
                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };            
            carControllerDb.ModelState.AddModelError("id", "not required");

            //ACT 
            var result = await carControllerDb.AddCarToDB(newCar);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            
            //ASSERT

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            repositoryMock.Verify(x => x.AddCarToDbAsync(It.IsAny<Car>()), Times.Never);

        }
        [Fact]
        public async Task DeleteCar_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {

            //ARRANGE
            var id = 0;
            carControllerDb.ModelState.AddModelError("id", "not in range");

            //ACT 
            var result = await carControllerDb.DeleteCarFromDb(id);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            
            //ASSERT
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            repositoryMock.Verify(x => x.UpdateCarInDbAsync(It.IsAny<Car>()), Times.Never);

        }

        [Fact]
        public async Task DeleteCar_ReturnsOktResult_WhenModelStateIsValid()
        {
            //ARRANGE
            var id = 0;

            //ACT         
            var result = await carControllerDb.DeleteCarFromDb(id);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            
            //ASSERT            
            Assert.IsType<OkObjectResult>(actionResult.Result);
            repositoryMock.Verify(r => r.DeleteCarFromDbAsync(id), Times.Once);
        }

        [Fact]
        public async Task AddCar_ReturnsCar_WhenModelStateIsValid()
        {
            //ARRANGE
            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };

            //ACT 
            var result = await carControllerDb.AddCarToDB(newCar);
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            //ASSERT

            Assert.IsType<OkObjectResult>(actionResult.Result);
            repositoryMock.Verify(x => x.AddCarToDbAsync(newCar), Times.Once);

        }

        [Fact]
        public async Task UpdateCar_ReturnsOK_WhenModelStateIsvalid()
        {
            //ARRANGE
            var newCar = new Car()
            {

                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };

            //ACT 
            var result = await carControllerDb.UpdateExistingCar(newCar);

            //ASSERT
            var actionResult = Assert.IsType<ActionResult<List<Car>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            repositoryMock.Verify(x => x.UpdateCarInDbAsync(newCar), Times.Once);

        }

        [Fact]
        public void GetAllCars()
        {

            var newCar = new Car()
            {
                Id = 1,
                BrandName = "FORD",
                Model = "MUSTANG",
                ManufactureYear = "1968"
            };
            IEnumerable<Car> carList = new List<Car>()
            {
                newCar
            }.AsEnumerable();
            
            //ARRANGE 
            repositoryMock.SetReturnsDefault(carList);

            //ACT 
            var returnResult = carControllerDb.GetCarsFromDb().Result;
            var ObjectResult = returnResult.Result;

            //ASSERT
            Assert.IsType<ActionResult<IEnumerable<Car>>>(returnResult);
            Assert.IsType<OkObjectResult>(ObjectResult);

        }
    }
}
