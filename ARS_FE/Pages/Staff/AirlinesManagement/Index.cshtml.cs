using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using Newtonsoft.Json;
using Service;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Flight;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<AllAirlinesResponseModel> Airline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            var url = "airlines";

            var response = await APIHelper.GetAsJsonAsync<ODataResponse<List<AllAirlinesResponseModel>>>(client, url);
            if (response != null)
            {
                Airline = PaginatedList<AllAirlinesResponseModel>.Create(response.Value, pageIndex ?? 1, 2);
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostChangeStatus(string id, string currentStatus, int pageIndex)
        {
            string newStatus = currentStatus == "Active" ? "Inactive" : "Active";
            var client = CreateAuthorizedClient();

            var response = await APIHelper.PutAsJson(client, $"airline/{id}/status", newStatus);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage(new { pageIndex });
            }
            else
            {
                return BadRequest();
            }
        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("OdataClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
