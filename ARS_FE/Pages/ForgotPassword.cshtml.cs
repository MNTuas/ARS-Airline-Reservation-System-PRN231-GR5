using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ARS_FE.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = default!;

        public ForgotPasswordModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await APIHelper.PutAsJson(client, "Auth/reset-password", Email);

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
