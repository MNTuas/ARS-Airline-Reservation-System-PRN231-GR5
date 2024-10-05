using BusinessObjects.RequestModels.Flight;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
        {
            await _flightService.CreateFlight(request);
            return Ok();
    }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(UpdateFlightRequest request, string id)
        {
            await _flightService.UpdateFlight(request, id);
            return Ok();
        }
    }
}

