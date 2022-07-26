using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace CarsAPI.Test.Controllers
{
    public class CarControllerDbTests3
    {
        private Mock<ICarInfoRepository> repositoryMock;
        private CarController carController;
        public CarControllerDbTests3()
        {
            repositoryMock = new Mock<ICarInfoRepository>();
            carController = new CarController(repositoryMock.Object);
        }
        [Fact]
        public void GetCarbyId_Returns_CarType()
        {
            //ARRANGE            
            repositoryMock.Setup(r => r.GetCarFromList(3)).ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 }); ;

            //ACT
            var result = carController.GetCarById(3).Result;

            //ASSERT
            repositoryMock.Verify(r => r.GetCarFromList(3));
            Assert.IsType<ActionResult<List<Car>>>(result);            
        }
    }
}
