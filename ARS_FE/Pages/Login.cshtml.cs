using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Enums;
using Service.Services.AuthService;
using System.Net.Http;

namespace ARS_FE.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginRequest request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await APIHelper.PostAsJson(client, "Auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<User>>();
                HttpContext.Session.SetString("JWToken", result.Message);
                if (result.Data.Role.Equals(UserRolesEnums.Staff.ToString()))
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

