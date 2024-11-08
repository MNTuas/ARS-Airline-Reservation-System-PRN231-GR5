using BusinessObjects.RequestModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARS_FE.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RegisterRequest request { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await APIHelper.PostAsJson(client, "Auth/register", request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while logging in");
                return Page();
            }
        }
    }
}
