using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Airport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.FlightManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
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
            var airlineList = new List<AllAirlinesResponseModel>();
            var airportList = new List<AirportResponseModel>();

            var responseAirline = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(client, "airline/Get_AllAirline");
            if (responseAirline != null)
            {
                airlineList = responseAirline;
            }
            var responseAirport = await APIHelper.GetAsJsonAsync<List<AirportResponseModel>>(client, "Airport/GetAll_Airport");
            if (responseAirport != null)
            {
                airportList = responseAirport;
            }

            ViewData["AirlinesId"] = new SelectList(airlineList, "Id", "Name");
            ViewData["From"] = new SelectList(airportList, "Id", "City");
            ViewData["To"] = new SelectList(airportList, "Id", "City");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.PostAsJson(client, "Flight", Flight);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the flight.");
                return Page();
            }
        }

        public async Task<JsonResult> OnGetAirplane(string id)
        {
            var client = CreateAuthorizedClient();
            var airline = new AirlinesResponseModel();
            var response = await APIHelper.GetAsJsonAsync<AirlinesResponseModel>(client, $"airline/{id}");

            if (response != null)
            {
                airline = response;
            }
            var airplaneList = airline.Airplanes;

            return new JsonResult(airplaneList);
        }

        public async Task<JsonResult> OnGetTicketClassPrices(string airplaneId)
        {
            var client = CreateAuthorizedClient();
            var responseSeatClass = await APIHelper.GetAsJsonAsync<AirplaneResponseModel>(client, $"airplane/get-airplane/{airplaneId}");

            var ticketClassPrices = new List<TicketClassPrice>();
            if (responseSeatClass != null)
            {
                foreach (var seatClass in responseSeatClass.AirplaneSeats)
                {
                    if (seatClass.SeatCount > 0)
                    {
                        ticketClassPrices.Add(new TicketClassPrice
                        {
                            SeatClassId = seatClass.SeatClassId,
                            SeatClassName = seatClass.SeatClassName,
                            TotalSeat = seatClass.SeatCount,
                            RemainSeat = seatClass.SeatCount,
                        });
                    }
                }
            }

            return new JsonResult(ticketClassPrices);
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
