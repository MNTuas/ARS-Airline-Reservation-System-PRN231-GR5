using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Service;
using System.Net.Http.Headers;

namespace ARS_FE.Pages
{
    public class TestingAfterLoginPageModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public TestingAfterLoginPageModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [BindProperty]
        public LoginRequest request { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            var client = _clientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return Page();
        }
    }
}
