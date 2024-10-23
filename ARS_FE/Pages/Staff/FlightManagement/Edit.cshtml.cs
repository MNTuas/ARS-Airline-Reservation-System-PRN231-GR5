using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Flight;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Flight;

namespace ARS_FE.Pages.Staff.FlightManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public UpdateFlightRequest Flight { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<FlightResponseModel>(client, $"Flight/{id}");
            var airportList = new List<AirportResponseModel>();

            var responseAirport = await APIHelper.GetAsJsonAsync<List<AirportResponseModel>>(client, "Airport/GetAll_Airport");
            if (responseAirport != null)
            {
                airportList = responseAirport;
            }
            if (response != null)
            {
                Flight = new UpdateFlightRequest
                {
                    FlightId = response.Id,
                    Duration = response.Duration,
                    DepartureTime = response.DepartureTime,
                    From = response.From,
                    To = response.To,
                    TicketClassPrices = response.TicketClassPrices.Select(t => new TicketClassPriceUpdate
                    {
                        Id = t.Id,
                        SeatClassName = t.SeatClassName,
                        Price = t.Price,
                    }).ToList()
                };
                ViewData["From"] = new SelectList(airportList, "Id", "City", response.From);
                ViewData["To"] = new SelectList(airportList, "Id", "City", response.To);
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();

            var updateModel = Flight;

            var response = await APIHelper.PutAsJson(client, $"Flight/{Flight.FlightId}", updateModel);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while update the airline.");
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
