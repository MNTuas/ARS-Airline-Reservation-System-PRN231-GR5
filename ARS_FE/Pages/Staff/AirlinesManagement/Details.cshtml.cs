using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using Service;
using System.Net.Http;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airplane;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public AirlinesResponseModel Airline { get; set; } = default!;
        public AirplaneResponseModel Airplane{ get; set; } = default!;
        public PaginatedList<AirplaneResponseModel> Airplanes { get; set; } = default!;
        public PaginatedList<AirplaneSeatResponse> seats { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string id, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<AirlinesResponseModel>(client, $"airline/{id}");

            if (response != null)
            {
                Airline = response;

                Airplanes = PaginatedList<AirplaneResponseModel>.Create(Airline.Airplanes, pageIndex ?? 1, 5);                
                
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
