using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        public ParkingController()
        {

        }


        [Route("balance")]
        [HttpGet]
        public string GetBalance()
        {
            return "Get";
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