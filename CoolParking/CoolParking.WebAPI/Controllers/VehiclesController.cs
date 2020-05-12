using CoolParking.WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public VehiclesController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var vehicles = _parkingService.GetVehicles();
            return Ok(vehicles);
        }

        [HttpPost]
        public IActionResult Post([FromBody] string vehicleId, [FromBody] int vehicleType, 
            [FromBody] decimal balance)
        {
            var type = VehicleTypeHelper.GetVehicleType(vehicleType);
            _parkingService.AddVehicle(new Vehicle(vehicleId, type, balance));
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromQuery] string vehicleId)
        {
            var vehicles = _parkingService.GetVehicle(vehicleId);
            return Ok(vehicles);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] string vehicleId)
        {
            _parkingService.RemoveVehicle(vehicleId);
            return Ok();
        }
    }
}