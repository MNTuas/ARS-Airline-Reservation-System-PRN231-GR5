using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Enums;
using Service.Services.AuthService;
using System.Net.Http;
using System.Security.Claims;

namespace ARS_FE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public LoginRequest request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await APIHelper.PostAsJson(client, "Auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<string>>();
                var token = result.Data;

                var userId = DecodeToken.DecodeTokens(token, "UserId");
                var role = DecodeToken.DecodeTokens(token, ClaimTypes.Role);
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("UserId", userId);
                if (role.Equals(UserRolesEnums.Staff.ToString()))
                {
                    return RedirectToPage("/Staff/Index");
                }
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while logging in");
                return Page();
            }
        }
    }
}

