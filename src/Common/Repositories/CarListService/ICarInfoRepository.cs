using Common.Entities;

namespace Common.Services.CarListService;

public interface ICarInfoRepository            //LISTING METHODS IMPLEMENTED IN INTERFACE
{
    Task<List<Car>> GetCarsFromList();
    Task<Car?> GetCarFromList(int Id);
    Task<List<Car>> AddCarToList(Car car);
    Task<List<Car>> UpdateCarInList(Car car);
    Task<List<Car>> DeleteCarFromList(int Id);
}
