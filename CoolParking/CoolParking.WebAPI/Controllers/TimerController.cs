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
    public class TimerController : ControllerBase
    {
        private readonly ITimerService _timerService;

        public TimerController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        [HttpPost]
        [HttpGet]
        [Route("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StartWithdraw()
        {
            _timerService.Interval = 5000;
            _timerService.Start();
            return Ok();
        }

        [HttpGet]
        [Route("stop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StopWithdraw()
        {
            _timerService.Stop();
            return Ok();
        }
    }
}