using BusinessObjects.Models;
using DAO;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services.FlightServices;
using System.IO;
using System.Text;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFlight : ControllerBase
    {
        private readonly IFlightService _flightService;

        public UploadFlight(IFlightService flightService)
        {

            _flightService = flightService;
        }

        [HttpPost("UploadExcelFile")]
        public async Task<IActionResult> UploadExcelFile([FromForm] IFormFile file)
        {
            var results =  await _flightService.UploadFile(file);
            if (results.Success != false)
            {
                return Ok(new
                {
                    results.Success,
                    results.Message,
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
