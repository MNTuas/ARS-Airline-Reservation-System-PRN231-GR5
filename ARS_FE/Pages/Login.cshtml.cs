using BusinessObjects.RequestModels.Auth;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Enums;
using System.Security.Claims;

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
        public LoginRequest request { get; set; } = default!;

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
                var name = DecodeToken.DecodeTokens(token, "Username");
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("UserId", userId);
                HttpContext.Session.SetString("Username", name);

                // Lấy ReturnUrl từ query string
                var returnUrl = Request.Query["ReturnUrl"].ToString();
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl); // Quay lại trang đã lưu
                }

                if (role.Equals(UserRolesEnums.Staff.ToString()))
                {
                    return RedirectToPage("/Staff/Index");
                }
                else if (role.Equals(UserRolesEnums.Admin.ToString()))
                {
                    return RedirectToPage("/Admin/Index");
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

