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

        public IndexModel()
        {
        }

        public PaginatedList<AllAirlinesResponseModel> Airline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await APIHelper.GetAsJsonAsync<List<AllAirlinesResponseModel>>(httpClient, APIHelper.Url + "airline");
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
        }

        public async Task<IActionResult> OnPostChangeStatus(string id, string currentStatus, int pageIndex)
        {
            using (var httpClient = new HttpClient())
            {
                string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

                var response = await APIHelper.PutAsJson(httpClient, APIHelper.Url + $"airline/{id}/status", newStatus);

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
}
