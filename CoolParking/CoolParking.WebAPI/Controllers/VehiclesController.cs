﻿using CoolParking.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.RegularExpressions;

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromQuery] string vehicleId)
        {
            bool isValidId = IsValidVechicleId(vehicleId);
            if(isValidId)
            {
                var deletedVehicle = _parkingService.RemoveVehicle(vehicleId);
                if(deletedVehicle != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest();
        }

        private bool IsValidVechicleId(string vehicleId)
        {
            Regex regex = new Regex(@"\w{2}-\d{4}-\w{2}", RegexOptions.IgnoreCase);
            return regex.IsMatch(vehicleId);
        }
    }
}