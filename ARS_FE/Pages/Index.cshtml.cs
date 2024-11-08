using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Airport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace ARS_FE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CreateFlightRequest Flight { get; set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            await LoadData();
            return Page();
        }

        private async Task LoadData()
        {
            var client = CreateAuthorizedClient();
            var airportList = new List<AirportResponseModel>();

            var responseAirport = await APIHelper.GetAsJsonAsync<List<AirportResponseModel>>(client, "Airport/GetAll_Airport");
            if (responseAirport != null)
            {
                airportList = responseAirport;
            }

            ViewData["From"] = new SelectList(airportList, "Id", "Name");
            ViewData["To"] = new SelectList(airportList, "Id", "Name");
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
