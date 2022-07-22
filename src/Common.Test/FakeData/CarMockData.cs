using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.FakeData
{
    public class CarMockData
    {
        public static List<Car> GetData()
        {
            return new List<Car>
            {
                new Car
                {
                    Id = 1,
                    BrandName="x",
                    ManufactureYear="2022",
                    Model="xx"
                },
                 new Car
                {
                    Id = 2,
                    BrandName="y",
                    ManufactureYear="2022",
                    Model="yy"
                },
                  new Car
                {
                    Id = 3,
                    BrandName="z",
                    ManufactureYear="2022",
                    Model="zz"
                }
            };
        }
    }
}
