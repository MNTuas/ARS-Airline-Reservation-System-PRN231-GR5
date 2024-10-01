using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.ResponseModels;
using Service;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class DetailsModel : PageModel
    {
        public DetailsModel()
        {
        }

        public AirlinesResponseModel Airline { get; set; } = default!;
        public PaginatedList<AirplaneResponseModel> Airplanes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var response = await APIHelper.GetAsJsonAsync<AirlinesResponseModel>(httpClient, APIHelper.Url + $"airline/{id}");

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
        }
    }

}
