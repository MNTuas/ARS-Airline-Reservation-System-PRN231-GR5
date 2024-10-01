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

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class IndexModel : PageModel
    {
        public PaginatedList<Airport> Airport{ get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await APIHelper.GetAsJsonAsync<List<Airport>>(httpClient, APIHelper.Url + "Airport");
                if (response != null)
                {
                    Airport = PaginatedList<Airport>.Create(response, pageIndex ?? 1, 6);
                    return Page();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        //public async Task<IActionResult> OnPostChangeStatus(string id, string currentStatus, int pageIndex)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string newStatus = currentStatus == "Active" ? "Inactive" : "Active";

        //        var response = await APIHelper.PutAsJson(httpClient, APIHelper.Url + $"Airport/{id}/status", newStatus);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToPage(new { pageIndex });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //}
    }
}
