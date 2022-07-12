using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test
{

    public class CarTests
    {
        [Fact]
        public void CheckCarClass_InputBrandName_CheckMaxLenghtBrandName()
        {
            //ARRANGE 
            var car = new Common.Entities.Car();

            //ACT
            car.BrandName = "asdadavvdfglkdfgdflgkjhfglkfgjhflgkhjflgkhjfghlerktretlkertejrterltj";

            //ASSERT
            Assert.NotInRange(car.BrandName.Length, 1, 25);
        }

        [Fact]
        public void CheckCarClass_InputYear_CheckMaxLenghtYear()
        {
            //ARRANGE 
            var car = new Common.Entities.Car();

            //ACT
            car.ManufactureYear = "19955";

            //ASSERT
            Assert.NotInRange(car.ManufactureYear.Length, 1, 4);
        }

        [Fact]
        public void CheckCarClass_InputModel_CheckMaxLenghtModel()
        {
            //ARRANGE 
            var car = new Common.Entities.Car();

            //ACT
            car.ManufactureYear = "asdadasasasdasdasfdgfdfgdfggdfdfgdgfdgferter";

            //ASSERT
            Assert.NotInRange(car.ManufactureYear.Length, 1, 25);
        }
    }
}
