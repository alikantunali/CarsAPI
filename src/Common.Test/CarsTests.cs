using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test
{

    public class CarTests
    {
        private Car _car;

        public CarTests()
        {
            _car =  new Common.Entities.Car();
        }
        [Fact]
        public void CheckCarClass_InputBrandName_CheckMaxLenghtBrandName()
        {
            //ARRANGE 
            

            //ACT
            _car.BrandName = "asdadavvdfglkdfgdflgkjhfglkfgjhflgkhjflgkhjfghlerktretlkertejrterltj";

            //ASSERT
            Assert.NotInRange(_car.BrandName.Length, 1, 25);
        }

        [Fact]
        public void CheckCarClass_InputYear_CheckMaxLenghtYear()
        {
            //ARRANGE 


            //ACT
            _car.ManufactureYear = "19955";

            //ASSERT
            Assert.NotInRange(_car.ManufactureYear.Length, 1, 4);
        }

        [Fact]
        public void CheckCarClass_InputModel_CheckMaxLenghtModel()
        {
            //ARRANGE 


            //ACT
            _car.ManufactureYear = "asdadasasasdasdasfdgfdfgdfggdfdfgdgfdgferter";

            //ASSERT
            Assert.NotInRange(_car.ManufactureYear.Length, 1, 25);
        }
    }
}
