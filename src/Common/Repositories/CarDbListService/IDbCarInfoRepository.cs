using Common.Entities;

namespace Common.Repositories.CarDbListService
{
    public interface IDbCarInfoRepository
    {
        //IEnumerable
        Task<IEnumerable<Car?>> GetCarsAsync();
        Task<Car?> GetCarByIdAsync(int carId);  // this can return null when id is given so ? mark is next to Car .

        Task<Car> AddCarAsync(Car request);

        Task<Car> UpdateCarAsync(Car request);
        Task<List<Car>> DeleteCarAsync(int carId);
    }
}
