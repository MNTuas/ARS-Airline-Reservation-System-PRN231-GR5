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

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Airport Airport { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = _httpClientFactory.CreateClient("ApiClient");


            var response = await APIHelper.GetAsJsonAsync<Airport>(client, $"airport/{id}");

            if (response != null)
            {
                Airport = response;


                return Page();
            }
            else
            {
                return BadRequest();
            }

        }
    }

}

