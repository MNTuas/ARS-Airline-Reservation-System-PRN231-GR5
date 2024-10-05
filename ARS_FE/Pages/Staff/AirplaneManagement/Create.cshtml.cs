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


        public async Task<IActionResult> OnGetAsync()
        {
            var client = CreateAuthorizedClient();

            // Gọi API để lấy danh sách Airlines
            var response = await APIHelper.GetAsJsonAsync<List<Airline>>(client, "airline");

            if (response != null)
            {
                //var airlines = await response.Content.ReadAsAsync<List<Airline>>();

                // Chuyển đổi danh sách airlines thành SelectList
                AirlinesList = response.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while fetching the airlines.");
            }

            return Page();
        }


        [BindProperty]
        public AddAirplaneRequest Airplane { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = CreateAuthorizedClient();

            var n = new AddAirplaneRequest
            {
                Code = Airplane.Code,
                AirlinesId = Airplane.AirlinesId,
                AvailableSeat = Airplane.AvailableSeat,
                Status = Airplane.Status,
                Type = Airplane.Type

            };

            var response = await APIHelper.PostAsJson(client, "airplane/add-airplane", n);

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
