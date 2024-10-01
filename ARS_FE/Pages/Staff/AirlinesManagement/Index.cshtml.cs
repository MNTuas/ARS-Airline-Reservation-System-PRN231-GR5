using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using Service.Services.AIrlineServices;
using Newtonsoft.Json;
using Service;
using BusinessObjects.ResponseModels;

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
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(client, "airline");
            if (response != null)
            {
                Airline = PaginatedList<AllAirlinesResponseModel>.Create(response, pageIndex ?? 1, 6);
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostChangeStatus(string id, string currentStatus, int pageIndex)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

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
    }
}
