using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarsAPI.Test
{
    public class CarControllerDbTests3
    {
        private Mock<ICarInfoRepository> _repositoryMock;
        private CarController _carController;
        public CarControllerDbTests3()
        {
            _repositoryMock = new Mock<ICarInfoRepository>();
            _carController = new CarController(_repositoryMock.Object);
        }
        [Fact]
        public async void GetCarbyId_Returns_CarType()
        {
            //Arrange            
            
            _repositoryMock.Setup(r=> r.GetCarFromList(3)).ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 }); ;

            //Act

            var result = _carController.GetCarById(3).Result;


            //Assert

            _repositoryMock.Verify(r => r.GetCarFromList(3));


            Assert.IsType<ActionResult<List<Car>>>(result);

        }
    }
}
