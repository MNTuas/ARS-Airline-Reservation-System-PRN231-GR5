using Microsoft.AspNetCore.Mvc;
using Service.Services.FlightServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFlight : ControllerBase
    {
        private readonly IFlightService _flightService;

        public UploadFlight(IFlightService flightService)
        {

            _flightService = flightService;
        }

        [HttpPost("UploadExcelFile")]
        public async Task<IActionResult> UploadExcelFile([FromForm] IFormFile file)
        {
            var results = await _flightService.UploadFile(file);
            if (results.Success != false)
            {
                return Ok(new
                {
                    results.Success,
                    results.Message,
                });
            }
            return BadRequest(new
            {
                results.Success,
                results.Message
            });
        }


    }
}
