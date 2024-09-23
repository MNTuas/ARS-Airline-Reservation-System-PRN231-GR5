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
        public async Task<IActionResult> GetAllAirlines()
        {
            var response = await _airlineService.GetAllAirlines();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAirlines([FromBody] string name)
        {
            await _airlineService.AddAirlines(name);
            return Ok("Add airline successfully");
        }
    }
}
