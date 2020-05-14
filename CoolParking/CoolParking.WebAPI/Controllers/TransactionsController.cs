using CoolParking.WebAPI.Interfaces;
using static CoolParking.WebAPI.Helpers.VehicleValidator;
using static CoolParking.WebAPI.Helpers.ExceptionMessageGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public TransactionsController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllTransactions()
        {
            var transactions = _parkingService.GetLastParkingTransactions();
            return Ok(transactions);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLastTransactions()
        {
            //NOT IMPLEMENT
            var transactions = _parkingService.GetLastParkingTransactions();
            return Ok(transactions);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TopUpVehicle(string id, decimal sum)
        {
            bool isValidId = IsValidVehicleId(id);
            if(isValidId)
            {
                var vehicle = _parkingService.TopUpVehicle(id, sum);
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