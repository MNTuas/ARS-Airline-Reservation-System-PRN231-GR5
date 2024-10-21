using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Flight;
using Repository.Repositories.AirporRepositories;
using Service.Services.AirportService;
using Service.Services.FlightServices;
using System.Net.Http.Headers;
using Service;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Airplane;

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

            var responseAirline = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(client, "airline");
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
                            SeatClassName = seatClass.SeatClassName
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
