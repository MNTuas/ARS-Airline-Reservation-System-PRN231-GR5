using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

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
        public AddAirplaneRequest Airplane { get; set; } = new AddAirplaneRequest();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadData();

            Airplane.AirplaneSeatRequest = new List<AirplaneSeatRequest>
            {
                new AirplaneSeatRequest { SeatClassId = "79562C9B-6B09-4CBF-B5A1-9903F2F15B67" }, 
                new AirplaneSeatRequest { SeatClassId = "96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9" }, 
                new AirplaneSeatRequest { SeatClassId = "977A3036-5375-44EB-9A62-411F3861F767" }  
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = CreateAuthorizedClient();

            var response = await APIHelper.PostAsJson(client, "airplane/add-airplane", Airplane);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                await LoadData();

                if (Airplane.AirplaneSeatRequest == null || Airplane.AirplaneSeatRequest.Count == 0)
                {
                    Airplane.AirplaneSeatRequest = new List<AirplaneSeatRequest>
                    {
                        new AirplaneSeatRequest { SeatClassId = "79562C9B-6B09-4CBF-B5A1-9903F2F15B67" },
                        new AirplaneSeatRequest { SeatClassId = "96A5D3DF-DE7B-4572-B8BD-AFE91DB378E9" },
                        new AirplaneSeatRequest { SeatClassId = "977A3036-5375-44EB-9A62-411F3861F767" }
                    };
                }

                ModelState.AddModelError(string.Empty, "Error occurred while creating the Airplane.");
                return Page();
            }
        }

        private async Task LoadData()
        {
            var client = CreateAuthorizedClient();
            var airlineList = new List<AllAirlinesResponseModel>();
            var seatClassList = new List<SeatClass>();

            // Lấy danh sách Airlines
            var responseAirline = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(client, "airline/Get_AllAirline");
            if (responseAirline != null)
            {
                airlineList = responseAirline;
            }

            // Lấy danh sách Seat Classes
            var responseSeatClass = await APIHelper.GetAsJsonAsync<List<SeatClass>>(client, "seat-class");
            if (responseSeatClass != null)
            {
                seatClassList = responseSeatClass;
            }

            ViewData["AirlinesId"] = new SelectList(airlineList, "Id", "Name");
            ViewData["SeatclassId"] = new SelectList(seatClassList, "Id", "Name");
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
