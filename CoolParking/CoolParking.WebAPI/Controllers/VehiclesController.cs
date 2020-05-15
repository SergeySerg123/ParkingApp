using CoolParking.WebAPI.Extensions;
using CoolParking.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static CoolParking.WebAPI.Helpers.VehicleValidator;
using static CoolParking.WebAPI.Helpers.ExceptionMessageGenerator;
using CoolParking.WebAPI.Models;
using System.Collections.Generic;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public VehiclesController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var vehicles = _parkingService.GetVehicles();
            return Ok(vehicles);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]VehicleSchema sh)
        {
            bool isValidId = IsValidVehicleId(sh.id);
            if(isValidId)
            {
                var type = VehicleTypeHelper.GetVehicleType(sh.vehicleType);
                var vehicle = _parkingService.AddVehicle(
                    Vehicle.CreateInstance(sh.id, type, sh.balance));
                if (vehicle != null)
                {
                    return Created("/api/vehicles/post", vehicle);
                }
            }          
            return BadRequest(GenereteAgrumentExceptionMessage());
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            bool isValidId = IsValidVehicleId(id);
            if (isValidId)
            {
                var vehicle = _parkingService.GetVehicle(id);
                if (vehicle != null)
                {
                    return Ok(vehicle.ToVehicleSchema());
                }
                return NotFound(GenereteAgrumentNullExceptionMessage());
            }
            
            return BadRequest(GenereteAgrumentExceptionMessage());
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            bool isValidId = IsValidVehicleId(id);
            if(isValidId)
            {
                var deletedVehicle = _parkingService.RemoveVehicle(id);
                if(deletedVehicle != null)
                {
                    return NoContent();
                }
                return NotFound(GenereteAgrumentNullExceptionMessage());
            }
            return BadRequest(GenereteAgrumentExceptionMessage());
        }

    }
}