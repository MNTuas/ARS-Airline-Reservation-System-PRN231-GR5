using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Flight;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using System.Net.Http.Headers;

namespace ARS_FE.Pages
{
    public class SearchFlightModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SearchFlightModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<FlightResponseModel> Flight { get; set; } = default!;

        [BindProperty]
        public CreateFlightRequest Flights { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateTime? checkin { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? checkout { get; set; }
        [BindProperty(SupportsGet = true)]
        public string from { get; set; }
        [BindProperty(SupportsGet = true)]
        public string to { get; set; }

        public async Task<IActionResult> OnGetAsync(string from, string to, DateTime checkin, DateTime? checkout, int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            List<FlightResponseModel> response;

            // Encode the DateTime values for URL
            var checkinEncoded = Uri.EscapeDataString(checkin.ToString("yyyy-MM-ddTHH:mm:ssZ"));

            // chọn url khi có checkout hoặc ko có
            if (checkout == null)
            {
                response = await APIHelper.GetAsJsonAsync<List<FlightResponseModel>>(client, $"Flight/search?from={from}&to={to}&checkin={checkinEncoded}");
            }
            else
            {
                var checkoutEncoded = Uri.EscapeDataString(checkout.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                response = await APIHelper.GetAsJsonAsync<List<FlightResponseModel>>(client, $"Flight/search?from={from}&to={to}&checkin={checkinEncoded}&checkout={checkoutEncoded}");
            }

            if (response != null)
            {
                await LoadData();
                Flight = PaginatedList<FlightResponseModel>.Create(response, pageIndex ?? 1, 6);
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
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
