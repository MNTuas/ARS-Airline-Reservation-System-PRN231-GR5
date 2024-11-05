using BusinessObjects.RequestModels.Airport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateAirportRequest createAirportRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = CreateAuthorizedClient();

            var n = new CreateAirportRequest
            {
                Name = createAirportRequest.Name,
                City = createAirportRequest.City,
                Country = createAirportRequest.Country,
            };

            var response = await APIHelper.PostAsJson(client, "Airport/AddNew_Airport", n);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the Airport.");
                return Page();
            }

        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

    }
}
