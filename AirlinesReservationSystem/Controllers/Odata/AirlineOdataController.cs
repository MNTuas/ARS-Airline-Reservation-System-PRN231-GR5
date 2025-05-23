﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Services.AirlineServices;

namespace AirlinesReservationSystem.Controllers.Odata
{
    [Route("odata/airlines")]
    [ApiController]
    public class AirlineOdataController : ODataController
    {
        private readonly IAirlineService _AirlineService;

        public AirlineOdataController(IAirlineService AirlineService)
        {
            _AirlineService = AirlineService;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllAirlinesDetails()
        {
            var result = await _AirlineService.GetAllAirlines();
            return Ok(result);
        }
    }
}
