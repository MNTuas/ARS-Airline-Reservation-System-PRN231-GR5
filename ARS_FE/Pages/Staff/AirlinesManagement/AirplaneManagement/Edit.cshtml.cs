using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Rank;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.AirplaneManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public UpdateAirplaneRequest UpdateAirplane { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var client = CreateAuthorizedClient();


            //var response = await APIHelper.GetAsJsonAsync<Airplane>(client, $"airplane/get-airplane/{id}");
            //if (response != null)
            //{
            //    UpdateAirplane = new UpdateAirplaneRequest
            //    {
                    //Id = response.Id,
                    //Type = response.Type,
                    //AirlinesId = response.AirlinesId,
                    //AvailableSeat = response.AvailableSeat,
                    //Code = response.Code,
                    //Status = response.Status,
                    
        //        };
        //        return Page();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();


            var response = await APIHelper.PutAsJson(client, $"airplane/update-airplane/{Id}", UpdateAirplane);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while update the airline.");
                return Page();
            }
        }

       
        private bool RankExists(string id)
        {
            return true;
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
