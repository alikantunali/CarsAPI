using Common.Entities;
using Common.Models;
using Microsoft.Extensions.Logging;

namespace Common.Repositories.CarListService;

public class CarInfoRepository : ICarInfoRepository //PERSISTENCE LOGIC APPLIED HERE.
{
        
    public CarInfoRepository()
    {
        
    }
    public async Task<List<Car>> GetCarsFromList()
    {
        var cars = CarsDataStore.cars.ToList();
        return cars;
    }

    public async Task<Car?> GetCarFromList(int carId)
    {
        if (carId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carId));
        }

        var car = CarsDataStore.cars.Find(i => i.Id == carId);
        if (car != null)
        {
            return car;
        }
        else
        {
            throw new Exception("No such car with the given Id.");
        }

    }

    public async Task<List<Car>> AddCarToList(Car car)
    {
        if (car.Id <= 3 && car.Model == null && car.ManufactureYear == null && car.BrandName == null)
        {
            throw new Exception("check car properties. Id must be greater than 3. ");

        }
        CarsDataStore.cars.Add(car);
        var cars = CarsDataStore.cars.ToList();
        return cars;

    }

    public async Task<List<Car>> DeleteCarFromList(int carId)
    {
        var car = CarsDataStore.cars.Find(i => i.Id == carId);
        if (car != null)
        {
            {
                CarsDataStore.cars.Remove(car);
                return CarsDataStore.cars;
            }

        }
        else
            throw new ArgumentOutOfRangeException(nameof(carId));

    }
    public async Task<List<Car>> UpdateCarInList(Car car)
    {

        if (car.Id <= 0)
        {
            throw new Exception("Given Id is invalid.");

        }
        else
        {
            var currentCar = CarsDataStore.cars.Find(c => c.Id == car.Id);

            if (currentCar != null)
            {
                currentCar.BrandName = car.BrandName;
                currentCar.ManufactureYear = car.ManufactureYear;
                currentCar.ManufactureYear = car.Model;
                return  CarsDataStore.cars.ToList();
            }
            else
            {
                throw new Exception("a car does not exist with the given ID");
            }

        }

    }
}
