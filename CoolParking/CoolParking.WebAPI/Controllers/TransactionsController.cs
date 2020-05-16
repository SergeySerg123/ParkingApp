using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using static CoolParking.WebAPI.Helpers.VehicleValidator;
using static CoolParking.WebAPI.Helpers.ExceptionMessageGenerator;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public TransactionsController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllTransactions()
        {
            var transactions = _parkingService.GetLastParkingTransactions();
            return Ok(transactions);
        }

        [HttpGet]
        [Route("last")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLastTransactions()
        {
            //NOT IMPLEMENT
            var transactions = _parkingService.GetLastParkingTransactions();
            return Ok(transactions);
        }

        [HttpPut]
        [Route("topupvehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TopUpVehicle([FromBody]TopUpSchema topUp)
        {
            bool isValidId = IsValidVehicleId(topUp.id);
            bool isValidSum = IsValidTopUpSum(topUp.sum);
            if (isValidId && isValidSum)
            {
                var vehicle = _parkingService.TopUpVehicle(topUp.id, topUp.sum);
                if (vehicle != null)
                {
                    return Ok();
                }
                return NotFound(GenereteAgrumentNullExceptionMessage());
            }
            return BadRequest(GenereteAgrumentExceptionMessage());
        }

    }
}