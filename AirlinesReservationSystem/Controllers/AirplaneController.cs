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
            var airplane = await _airplaneService.GetAirlane(id);
            if (airplane == null)
            {
                return NotFound();
            }
            return Ok(airplane);

        }
        [HttpPost]
        [Route("add-airplane")]
        public async Task<IActionResult> AddAirplane([FromBody]AddAirplaneRequest addAirplane)
        {
            var result = await _airplaneService.AddAirplane(addAirplane);
            if (result)
            {
                return Ok("Add Airplane success");
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("update-airplane/{id}")]
        public async Task<IActionResult> UpdateAirplane(string id,[FromBody]UpdateAirplaneRequest updateAirplane)
        {
            var result = await _airplaneService.UpdateAirplane(id, updateAirplane);
            if (result) {
                return Ok("Update Airplane success");
            }
            return BadRequest();
        }
    }
}
