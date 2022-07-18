using CarsAPI.Controllers;
using Common.Entities;
using Common.Repositories;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test
{
    public class CarInfoMoqRepositoryTests
    {
        [Fact]
        public async void GetCarbyId_Returns_CarType()
        {
            //Arrange
            
            var repositoryMock = new Mock<ICarInfoRepository>();
            repositoryMock.Setup(r=> r.GetCarFromList(3));

            var carController = new CarController(repositoryMock.Object);


            //Act

            var result = carController.GetCarById(3).Result;
            
            //Assert

            repositoryMock.Verify(r => r.GetCarFromList(3));


            Assert.IsType<ActionResult<List<Common.Entities.Car>>>(result);

        }
    }
}
