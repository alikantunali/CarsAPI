using Common.Entities;
using Common.Repositories.CarListService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test.Repositories
{
    public class CarInfoTestRepository : ICarInfoRepository
    {
        private List<Common.Entities.Car> _carList;

        public CarInfoTestRepository()
        {
            _carList = new()
            {
                new Common.Entities.Car
              {
                  Id =1,
                  BrandName = "TOYOTA",
                  ManufactureYear = "1999",
                  Model = "Supra"
              },
                 new Common.Entities.Car
              {
                  Id =2,
                  BrandName = "HONDA",
                  ManufactureYear = "2002",
                  Model = "S2000"
              },
                 new Common.Entities.Car
              {
                  Id =3,
                  BrandName = "BMW",
                  ManufactureYear = "2022",
                  Model = "M5"
              }
            };
        }

        public Task<List<Common.Entities.Car>> AddCarToList(Common.Entities.Car car)
        {
            _carList.Add(car);
            return Task.FromResult(_carList);
        }

        public Task<List<Common.Entities.Car>> DeleteCarFromList(int Id)
        {
            _carList.Remove(_carList[Id]);
            return Task.FromResult(_carList);

        }

        public Task<Common.Entities.Car?> GetCarFromList(int Id)
        {
            var car = _carList[Id];
            return Task.FromResult(car);

        }

        public Task<List<Common.Entities.Car>> GetCarsFromList()
        {
            return Task.FromResult(_carList);
        }

        public Task<List<Common.Entities.Car>> UpdateCarInList(Common.Entities.Car car)
        {
            var updatedCar = _carList.Find(c => c.Id == car.Id);
            updatedCar.BrandName = car.BrandName;
            updatedCar.ManufactureYear = car.ManufactureYear;
            updatedCar.Model = car.Model;
            return Task.FromResult(_carList);

        }
    }
}
