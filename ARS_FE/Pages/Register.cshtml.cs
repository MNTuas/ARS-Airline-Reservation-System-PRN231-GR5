using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Services.AuthService;

namespace ARS_FE.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public RegisterModel(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        [BindProperty]
        public RegisterRequest Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _authService.RegisterAsync(Input);
            if (result.Success)
            {
                return RedirectToPage("/Login");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }
    }
}
