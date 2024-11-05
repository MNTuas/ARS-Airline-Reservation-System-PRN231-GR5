using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Airport Airport { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();


            var response = await APIHelper.GetAsJsonAsync<Airport>(client, $"Airport/GetAirportById/{id}");



            if (response != null)
            {
                Airport = response;
                return Page();
            }
            else
            {
                return BadRequest();
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

