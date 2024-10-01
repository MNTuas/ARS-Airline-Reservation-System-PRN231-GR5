using BusinessObjects.RequestModels;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service.Services.AuthService;

namespace ARS_FE.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
        }
        [BindProperty]
        public LoginRequest request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await APIHelper.PostAsJson(httpClient, APIHelper.Url + "Auth/login", request);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToPage("/TestingAfterLoginPage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while logging in");
                    return Page();
                }
            }
        }
    }
}
