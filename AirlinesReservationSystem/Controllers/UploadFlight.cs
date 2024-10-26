using BusinessObjects.Models;
using DAO;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
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
            await _flightService.UploadFile(file);
            return Ok();
        }


    }
}
