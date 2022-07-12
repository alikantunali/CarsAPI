using Common.Entities;
using Common.Repositories.CarListService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers                           // CONTROL DOES DEPENDENCY INJECTION NOT TO DUPLICATE ANY CODE SUCH AS ERROR RESPONSE OR
                                                        // OTHER INSERT UPDATE DELETE SO ON. (SERVICES)
                                                        // this controller will do a dependency injection from DataContext.cs 
{
    [Route("StaticCarsAPI")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly ICarInfoRepository _carInfoRepository;

        public CarController(ICarInfoRepository carInfoRepository)
        {
            _carInfoRepository = carInfoRepository ?? throw new ArgumentNullException(nameof(carInfoRepository));            
        }


        [HttpGet("ListCars")]
        //public async Task<IActionResult> Get() //NO SCHEMAS DEFINED HERE WITH IActionResult
        public async Task<ActionResult<IEnumerable<Car>>> ListCars()
        {

            var carsToReturn = await _carInfoRepository.GetCarsFromList();
            return Ok(carsToReturn);

        }

        
        [HttpGet("GetCarById/{id}")]                              //WHEN ID IS HERE , IT BECOMES REQUIRED
                                                                //public async Task<IActionResult> Get() //NO SCHEMAS DEFINED HERE WITH IActionResult
        public async Task<ActionResult<List<Car>>> GetCarById(int id)
        {

            var carResult = await _carInfoRepository.GetCarFromList(id);
            return Ok(carResult);

        }

        [HttpPost("AddCar")]
        public async Task<ActionResult<List<Car>>> AddCar([FromBody] Car car)
        {

           await _carInfoRepository.AddCarToList(car);
            return Ok($"{car.ManufactureYear}/{car.BrandName}/{car.Model} added to list.");

        }

        [HttpPut("UpdateCar")]
        public async Task<ActionResult<List<Car>>> UpdateCar([FromBody] Car car)
        {

            var result = await _carInfoRepository.UpdateCarInList(car);
            return Ok($"{car.ManufactureYear}/{car.BrandName}/{car.Model} added to list.\n {result}");

        }

        [HttpDelete("DeleteCar/{id}")]
        public async Task<ActionResult<List<Car>>> DeleteCar(int id)
        {
            await _carInfoRepository.DeleteCarFromList(id);
            return Ok($"Car with {id} deleted");

        }

    }
}
