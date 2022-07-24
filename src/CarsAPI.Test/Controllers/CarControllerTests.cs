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
        public async void GetCarbyId_Returns_CarType()
        {
            //Arrange            

            repositoryMock.Setup(r => r.GetCarFromList(3)).ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 }); ;

            //Act

            var result = carController.GetCarById(3).Result;


            //Assert

            repositoryMock.Verify(r => r.GetCarFromList(3));


            Assert.IsType<ActionResult<List<Car>>>(result);

        }
    }
}
