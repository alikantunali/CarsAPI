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
        private List<Car> _carList;

        public CarInfoTestRepository()
        {
           _carList = new()
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

        public Task<List<Car>> AddCarToList(Car car)
        {
            _carList.Add(car);
            return Task.FromResult(_carList);
        }

        public Task<List<Car>> DeleteCarFromList(int Id)
        {
            _carList.Remove(_carList[Id]);
            return Task.FromResult(_carList);

        }

        public Task<Car?> GetCarFromList(int Id)
        {
            var car = _carList[Id];
            return Task.FromResult(car);

        }

        public Task<List<Car>> GetCarsFromList()
        {
            return Task.FromResult(_carList);
        }

        public Task<List<Car>> UpdateCarInList(Car car)
        {
            var updatedCar = _carList.Find(c => c.Id == car.Id);
            updatedCar.BrandName = car.BrandName;
            updatedCar.ManufactureYear = car.ManufactureYear;
            updatedCar.Model = car.Model;
            return Task.FromResult(_carList);

        }
    }
}
