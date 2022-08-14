using Common.Entities;
using Common.Repositories.CarDbListService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// CONTROL DOES DEPENDENCY INJECTION NOT TO DUPLICATE ANY CODE SUCH AS ERROR RESPONSE OR OTHER INSERT UPDATE DELETE SO ON. (SERVICES).
// this controller will do a dependency injection from db method interface
namespace CarsAPI.Controllers

{

    [Route("CarsAPI")]  
    [ApiController]
    [ResponseCache(CacheProfileName = "VaryUserAgentHeader_Default30")]
    //CONSTRUCTOR IS NEEDED TO INJECT DATACONTEXT and SERVICES BELONG TO INTERFACE IN THE STARTUP
    public class CarControllerDb : ControllerBase
    {
        private readonly IDbCarInfoRepository _dbCarInfoRepository;

        //DEPENDENCY INJKECTION FOR DB CONNECTION
        public CarControllerDb(IDbCarInfoRepository dbCarInfoRepository)
        {
            _dbCarInfoRepository = dbCarInfoRepository ?? throw new ArgumentNullException(nameof(dbCarInfoRepository));
        }

        
    
        [HttpGet("allCars")]
        [ResponseCache(CacheProfileName = "VaryUserAgentHeader_Default30")]
        //public async Task<IActionResult> Get() //NO SCHEMAS DEFINED HERE WITH IActionResult

        //GET THE CARS FROM CARS TABLE BY USING DB SET "CARS" DEFINED IN DataContext.cs
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsFromDb()
        {
            try
            {
                var cars = await _dbCarInfoRepository.GetCarsFromDbAsync();
                if (cars != null)
                {
                    return Ok(cars);
                }
                return NoContent(); 
            }       
            catch
            {
                return NotFound("Connection to DB is not configured. Run Migration and specify Connection String Properly.");
            }

        }

        [HttpGet("car/{carId}")]
        [ResponseCache(CacheProfileName = "VaryUserAgentHeader_Default30")]
        public async Task<ActionResult<Car>> GetCarFromDB( int id)
            
        {
            if (id <=0)
            {
                return BadRequest();
            }
            else
            {
                var car = await _dbCarInfoRepository.GetCarByIdFromDbAsync(id);
                if (car != null)
                {
                    return Ok(car);
                }
                return NoContent();

            }                                  

        }

        [HttpPost("addCar")]
        public async Task<ActionResult<List<Car>>> AddCarToDB(Car request)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dbCarInfoRepository.AddCarToDbAsync(request);                   

            return Ok("New car added to db");
        }
     
        [HttpPut("updateCar")]
        public async Task<ActionResult<List<Car>>> UpdateExistingCar(Car request)
        {
            try
            {
                await _dbCarInfoRepository.UpdateCarInDbAsync(request);
                
                return Ok("Update is succesful");
            }
            catch
            {                
                return BadRequest("Given request is not valid. Check parameters.");
            }
        }

        [HttpDelete("deleteCar/{carId}")]
        public async Task<ActionResult<List<Car>>> DeleteCarFromDb(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
                try
            {
                await _dbCarInfoRepository.DeleteCarFromDbAsync(id);
                return Ok("Delete successful.");
            }
                catch
                {
                    return BadRequest(id);
                }

        }
    /*
            /// <summary>
            /// WILL BE UPDATED FOR PATCHING
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            [Route("CarsAPI/UpdateCarPartially")]    
            [HttpPatch]
            public async Task<ActionResult<List<Car>>> UpdateCarPartially(Car request)
            {
                if (request.Id <= 0)
                {
                    return BadRequest("Id is invalid.");

                }
                else
                {

                    var dbCar = await _dataContext.Cars.FindAsync(request.Id);

                    if (dbCar != null)
                    {

                        dbCar.BrandName = request.BrandName;
                        dbCar.ManufactureYear = request.ManufactureYear;
                        dbCar.Model = request.Model;

                        await _dataContext.SaveChangesAsync();

                        return Ok("Update is succesful");
                    }
                    return NotFound("a car does not exist with the given ID");
                }                           

            }
           */
    //---------------------------------------------------------------------------------------------------
    }
}
