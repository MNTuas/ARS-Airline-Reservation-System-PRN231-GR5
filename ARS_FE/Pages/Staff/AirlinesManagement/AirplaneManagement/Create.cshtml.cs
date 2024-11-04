using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Rank;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airport;

namespace ARS_FE.Pages.Staff.AirplaneManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<SelectListItem> AirlinesList { get; set; } = new List<SelectListItem>();
        
        [BindProperty]
        public AddAirplaneRequest Airplane { get; set; } = default!;

        [BindProperty]
        public SeatClass SeatClass { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
           await LoadData();
            // Initialize the AirplaneSeatRequest list
            Airplane = new AddAirplaneRequest
            {
                AirplaneSeatRequest = new List<AirplaneSeatRequest>()
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var client = CreateAuthorizedClient();

            //var n = new AddAirplaneRequest
            //{
            //    CodeNumber = Airplane.CodeNumber,
            //    AirlinesId = Airplane.AirlinesId,
            //    AirplaneSeatRequest = Airplane.AirplaneSeatRequest,

            //};

            var response = await APIHelper.PostAsJson(client, "airplane/add-airplane", Airplane);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToPage("./Index");
            }
            else
            {
                await LoadData();
                // Initialize the AirplaneSeatRequest list
                Airplane = new AddAirplaneRequest
                {
                    AirplaneSeatRequest = new List<AirplaneSeatRequest>()
                };
                ModelState.AddModelError(string.Empty, "Error occurred while creating the Airport.");
                return Page();
            }
        }

        private async Task LoadData()
        {
            var client = CreateAuthorizedClient();
            var airlineList = new List<AllAirlinesResponseModel>();
            var airportList = new List<AirportResponseModel>();
            var seatClass = new List<SeatClass>();

            var responseAirline = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(client, "airline/Get_AllAirline");
            if (responseAirline != null)
            {
                airlineList = responseAirline;
            }

            var responseSeatClass = await APIHelper.GetAsJsonAsync<List<SeatClass>>(client, "seat-class");
            if (responseSeatClass != null)
            {
                seatClass = responseSeatClass;
            }

            ViewData["AirlinesId"] = new SelectList(airlineList, "Id", "Name");
            ViewData["SeatclassId"] = new SelectList(seatClass, "Id", "Name");
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
