using BusinessObjects.RequestModels;
using BusinessObjects.RequestModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AuthService;
using Service.Services.UserServices;
using System.Security.Claims;

namespace AirlinesReservationSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var results = await _authService.RegisterAsync(request);
            if (results.Success != false)
            {
                return Ok(new
                {
                    Status = results.Success,
                    Message = results.Message,
                    Data = results.Data
                });
            }
            return BadRequest(new
            {
                Status = results.Success,
                Message = results.Message
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var results = await _authService.LoginAsync(request);
            if (results.Success != false)
            {
                return Ok(new
                {
                    Status = results.Success,
                    Message = results.Message,
                    Data = results.Data
                });
            }
            return BadRequest(new
            {
                Status = results.Success,
                Message = results.Message
            });
        }
        [HttpPut]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            string currentUserId = HttpContext.User.FindFirstValue("UserId");
            var result = await _authService.ChangePassword(currentUserId, request);
            if (result.Success != false)
            {
                return Ok(new
                {
                    Status = result.Success,
                    Message = result.Message,
                });
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("reset-password")]
        public async Task<IActionResult> SendEmailResetPassword([FromBody] string email)
        {
            await _userService.SendEmailWhenForgotPassword(email);
            return Ok("Success");
        }
    }
}
