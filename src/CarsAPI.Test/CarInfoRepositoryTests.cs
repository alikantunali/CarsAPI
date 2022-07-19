using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CarsAPI.Test
{
    public class CarInfoRepositoryTests
    {
        [Fact]
        public async void GetCarbyId_Returns_CarType()
        {
            //Arrange
            
            var repositoryMock = new Mock<ICarInfoRepository>();
            repositoryMock.Setup(r=> r.GetCarFromList(3)).ReturnsAsync(new Car { BrandName = "FORD", ManufactureYear = "MUSTANG", Model = "1968", Id = 3 }); ;

            var carController = new CarController(repositoryMock.Object);


            //Act

            var result = carController.GetCarById(3).Result;
            

            //Assert

            repositoryMock.Verify(r => r.GetCarFromList(3));


            Assert.IsType<ActionResult<List<Car>>>(result);

        }
    }
}
