using BusinessObjects.RequestModels.Airplane;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Service.Services.AirplaneServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/airplane")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneService _airplaneService;
        public AirplaneController(IAirplaneService airplaneService)
        {
            _airplaneService = airplaneService;
        }
        [HttpGet]
        [Route("get-all-airplane")]
        public async Task<IActionResult> GettAllAirplane()
        {
            return Ok(await _airplaneService.GetAllAirplane());
        }
        [HttpGet]
        [Route("get-airplane/{id}")]
        public async Task<IActionResult> GetAirplane(string id)
        {
            var airplane = await _airplaneService.GetAirplane(id);
            if (airplane == null)
            {
                return NotFound();
            }
            return Ok(airplane);

        }
        [HttpPost]
        [Route("add-airplane")]
        public async Task<IActionResult> AddAirplane([FromBody] AddAirplaneRequest addAirplane)
        {
            await _airplaneService.AddAirplane(addAirplane);
            return Ok("Add Airplane success");
        }

        [HttpPut]
        [Route("update-airplane/{id}")]
        public async Task<IActionResult> UpdateAirplane(string id, [FromBody] UpdateAirplaneRequest updateAirplane)
        {
            await _airplaneService.UpdateAirplaneAsync(id, updateAirplane);
            return Ok("Update Airplane success");
        }
    }
}
