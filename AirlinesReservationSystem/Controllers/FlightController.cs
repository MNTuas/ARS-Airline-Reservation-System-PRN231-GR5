using BusinessObjects.RequestModels.Flight;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(string id)
        {
            var result = await _flightService.GetFlightById(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetFlightByFilter(string from, string to, DateTime checkin, DateTime? checkout)
        {
            var flights = await _flightService.GetFlightByFilter(from, to, checkin, checkout);

            if (flights == null)
            {
                return NotFound("No flights found matching the criteria.");
            }

            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
        {
            await _flightService.CreateFlight(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(UpdateFlightRequest request, string id)
        {
            await _flightService.UpdateFlight(id, request);
            return Ok();
        }
    }
}

