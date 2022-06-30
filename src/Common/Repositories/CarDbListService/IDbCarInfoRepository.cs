using Common.Entities;

namespace Common.Services.CarDbListService
{
    public interface IDbCarInfoRepository
    {
        Task<IEnumerable<Car>> GetCarsFromDbAsync();
        Task<Car?> GetCarByIdFromDbAsync(int carId);  // this can return null when id is given so ? mark is next to Car .

        Task<Car?> AddCarToDbAsync(Car request);

        Task<Car?> UpdateCarInDbAsync(Car request);
        Task<List<Car>> DeleteCarFromDbAsync(int carId);
    }
}
