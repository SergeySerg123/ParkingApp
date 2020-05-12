using System.Net.Mime;
using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }


        [Route("balance")]
        [HttpGet]
        public IActionResult GetBalance()
        {
            decimal balance = _parkingService.GetBalance();
            if (balance < 0) { return BadRequest(); }
            return Ok(balance);
        }

        [Route("capacity")]
        [HttpGet]
        public IActionResult GetCapacity()
        {
            var capacity = _parkingService.GetCapacity();
            if (capacity < 0 || capacity > Settings.Capacity) { return BadRequest(); }
            return Ok(capacity);
        }

        [Route("freePlaces")]
        [HttpGet]
        public IActionResult GetFreePlaces()
        {
            var freePlaces = _parkingService.GetFreePlaces();
            if (freePlaces < 0) { return BadRequest(); }
            return Ok(freePlaces);
        }
    }
}