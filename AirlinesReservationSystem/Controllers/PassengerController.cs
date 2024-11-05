using Azure;
using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.RequestModels.Passenger;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.PassengerServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _PassengerService;

        public PassengerController(IPassengerService PassengerService)
        {
            _PassengerService = PassengerService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreatePassenger(CreatePassengerRequest createPassengerRequest)
        {

            var result = await _PassengerService.addPassenger(createPassengerRequest);

            if (result.Success)
            {
                return Ok(new
                {
                    message = result.Message,
                    tickets = result.Data
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }
        }
        
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPassengerInfo(string id)
        {
            var response = await _PassengerService.GetDetailsPassengerInfo(id);
            return Ok(response);
        }

        [HttpGet("GetPassengerByLogin")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPassengerByLogin()
        {
            var response = await _PassengerService.GetPassengerByLogin();
            return Ok(response);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllPassengers()
        {
            var response = await _PassengerService.GetAllPassengers();
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePassenger(string id, UpdatePassengerRequest request)
        {
            await _PassengerService.UpdatePassenger(id, request);
            return Ok("Update passenger successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePassenger(string id)
        {
            await _PassengerService.DeletePassenger(id);
            return Ok("Delete passenger successfully");
        }
    }
}
