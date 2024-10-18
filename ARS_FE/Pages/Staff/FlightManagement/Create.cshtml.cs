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

namespace ARS_FE.Pages.Staff.FlightManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGet()
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
            return Page();
        }

        [BindProperty]
        public CreateFlightRequest Flight { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = CreateAuthorizedClient();

            var request = Flight;

            var response = await APIHelper.PostAsJson(client, "Flight", request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the airline.");
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
