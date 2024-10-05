using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AuthService;

namespace AirlinesReservationSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}
