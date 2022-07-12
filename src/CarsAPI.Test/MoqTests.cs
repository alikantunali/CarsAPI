using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Models;
using Common.Repositories.CarListService;
using CarsAPI.Test.Repositories;

namespace CarsAPI.Test
{
    public class MoqTests
    {
        [Fact]
        public async Task FetchInternalCar_CarFetched()
        {
            //Arrange             

            var carInfoTestDataRepositoryMock = new Mock<CarInfoTestRepository>();
            var carInfoRepositoryMock = new Mock<ICarInfoRepository>();
            var carInfoRepository = new CarInfoRepository();
            
            
            carInfoTestDataRepositoryMock.Setup(m => m.GetCarsFromList()).Returns(new CarInfoRepository().GetCarsFromList);
                        

            //var carInfo = carInfoTestDataRepositoryMock.Object;
                        
            //Act        
            var carList= carInfoRepository.GetCarsFromList();
            
            //Assert
            Assert.Contains("BMW",carList.ToString());

        }
        


    }
}
