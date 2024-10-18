
using BusinessObjects.RequestModels.Airport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AirportService;

namespace AirportReservationSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("GetAll_Airport")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllAirport()
        {
            var response = await _airportService.GetAllAirport();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAirportById/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAirportInfo(string id)
        {
            var response = await _airportService.GetDetailsAirportInfo(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("Update_Airport/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateAirport(string id, UpdateAirportRequest updateAirportRequest)
        {
            await _airportService.UpdateAirports(id, updateAirportRequest);
            return Ok("Update airport successfully");
        }

        [HttpPut]
        [Route("ChangeStatusAirport/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> ChangeAirportStatus(string id)
        {
            await _airportService.ChangeAirportsStatus(id);
            return Ok("Update Airport's status successfully");
        }

        [HttpPost("AddNew_Airport")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AddNewAirport(CreateAirportRequest createAirportRequest)
        {
            var results = await _airportService.AddAirport(createAirportRequest);
            if (results.Success != false)
            {
                return Ok(new
                {
                    results.Success,
                    results.Message,
                    results.Data
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
