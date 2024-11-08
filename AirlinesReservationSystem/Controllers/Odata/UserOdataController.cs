using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Services.UserServices;

namespace AirlinesReservationSystem.Controllers.Odata
{
    [Route("odata/users")]
    [ApiController]
    public class UserOdataController : ODataController
    {
        private readonly IUserService _userService;

        public UserOdataController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserExceptAdmin()
        {
            var response = await _userService.GetAllUserExceptAdmin();
            return Ok(response);
        }
    }
}
