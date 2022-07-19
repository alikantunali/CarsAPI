using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Common.Test
{
    public class MockedDataDbControllerTests 
    {
        private Mock<IDbCarInfoRepository> _service;
        private CarControllerDb _carControllerDb;

        public MockedDataDbControllerTests()
        {
            _service = new Mock<IDbCarInfoRepository>();
            _carControllerDb = new CarControllerDb(_service.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            /// Arrange

            _service.Setup(_ => _.GetCarsFromDbAsync()).ReturnsAsync(CarMockData.GetData());
            

            /// Act
            var result = _carControllerDb.GetCarsFromDb();
            var actionResult = await result;
            var okResult = actionResult.Result;
            

            // /// Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn204NoContentStatus()
        {
            /// Arrange

            _service.Setup(_ => _.GetCarsFromDbAsync()).ReturnsAsync(CarMockNoData.GetEmptyCarList());

            /// Act
            var result = _carControllerDb.GetCarsFromDb().Result;
            var actionResult = result.Result;
            var noContentResult = actionResult;
            
            /// Assert
            Assert.IsType<NoContentResult>(noContentResult);
            _service.Verify(_ => _.GetCarsFromDbAsync(), Times.Exactly(1));
        }
    }
}
