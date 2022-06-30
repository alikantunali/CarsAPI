using Common.Entities;
namespace Common.Models
{
    public class CarsDataStore
    {

        public static List<Car> cars = new List<Car>
          {
              new Car
              {
                  Id =1,
                  BrandName = "TOYOTA",
                  ManufactureYear = "1999",
                  Model = "Supra"
              },
                 new Car
              {
                  Id =2,
                  BrandName = "HONDA",
                  ManufactureYear = "2002",
                  Model = "S2000"
              },
                 new Car
              {
                  Id =3,
                  BrandName = "BMW",
                  ManufactureYear = "2022",
                  Model = "M5"
              }
          };
    }
}
