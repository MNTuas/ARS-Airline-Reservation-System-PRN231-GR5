using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllFlightClassse()
        {
            var response = await _flightClassService.GetAllFlightClassse();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetFlightClassById(string id)
        {
            var response = await _flightClassService.GetFlightById(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateFlightClass([FromBody] FlightClassRequest request, string id)
        {
            var res = await _flightClassService.UpdateFlightClass(request, id);
            return Ok(res);
        }

        [HttpDelete("Delete_FlightClass")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteFlightClass(string id)
        {
            var result = await _flightClassService.DeleteFlightClass(id);
            if (result.Success != false) 
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AddNewFlightClass([FromBody] FlightClassRequest request)
        {
            var res = await _flightClassService.AddFlightClass(request);
            return Ok(res);
        }
    }
}
