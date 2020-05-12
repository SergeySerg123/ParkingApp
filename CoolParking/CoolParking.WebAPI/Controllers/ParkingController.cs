using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolParking.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(balance);
        }

        [Route("capacity")]
        [HttpGet]
        public string GetCapacity()
        {
            return "Get";
        }

        [Route("freePlaces")]
        [HttpGet]
        public string GetFreePlaces()
        {
            return "Get";
        }
    }
}