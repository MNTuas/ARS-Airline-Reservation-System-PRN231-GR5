using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AIrlineServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/airline")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllAirlines()
        {
            var response = await _airlineService.GetAllAirlines();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAirlinesInfo(string id)
        {
            var response = await _airlineService.GetDetailsAirlineInfo(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateAirlines(string id, [FromBody] string name)
        {
            await _airlineService.UpdateAirlines(id, name);
            return Ok("Update airline successfully");
        }

        [HttpPut]
        [Route("{id}/status")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> ChangeAirlinesStatus(string id, [FromBody] string status)
        {
            await _airlineService.ChangeAirlinesStatus(id, status);
            return Ok("Update airlines's status successfully");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AddNewAirlines([FromBody] string name)
        {
            await _airlineService.AddAirlines(name);
            return Ok("Add airline successfully");
        }
    }
}
