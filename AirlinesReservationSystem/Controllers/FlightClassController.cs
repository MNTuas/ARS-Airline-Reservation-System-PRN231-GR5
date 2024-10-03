using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.FlightClassServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightClassController : ControllerBase
    {
        private readonly IFlightClassService _flightClassService;
        public FlightClassController(IFlightClassService flightClassService)
        {
            _flightClassService = flightClassService;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetFlightClassById(string id)
        {
            var response = await _flightClassService.GetFlightById(id);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllFlightClassse()
        {
            var response = await _flightClassService.GetAllFlightClassse();
            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AddNewFlightClass([FromBody] FlightClassRequest request)
        {
            var res = await _flightClassService.AddFlightClass(request);
            return Ok(res);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateAirlines([FromBody] FlightClassRequest request, string id)
        {
            var res = await _flightClassService.UpdateFlightClass(request, id);
            return Ok(res);
        }
    }
}
