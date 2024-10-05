using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.FlightServices;

namespace AirlinesReservationSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlightsDetails()
        {
            var result = await _flightService.GetAllFlightsDetails();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(string id)
        {
            var result = await _flightService.GetFlightById(id);
            return Ok(result);
        }
    }
}

