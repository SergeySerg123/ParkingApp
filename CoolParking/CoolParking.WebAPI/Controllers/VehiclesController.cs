﻿using CoolParking.WebAPI.Extensions;
using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var vehicles = _parkingService.GetVehicles();
            return Ok(vehicles);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] string vehicleId, [FromBody] int vehicleType, 
            [FromBody] decimal balance)
        {
            var type = VehicleTypeHelper.GetVehicleType(vehicleType);
            var vehicle = _parkingService.AddVehicle(
                Vehicle.CreateInstance(vehicleId, type, balance));
            if (vehicle != null)
            {
                return Created("/api/vehicles/post", vehicle);
            }
            return BadRequest();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromQuery] string vehicleId)
        {
            bool isValidId = IsValidVechicleId(vehicleId);
            if (isValidId)
            {
                var vehicle = _parkingService.GetVehicle(vehicleId);
                if (vehicle != null)
                {
                    return Ok(vehicle.ToVehicleSchema());
                }
                return NotFound();
            }
            
            return BadRequest();
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