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
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Airport;

namespace ARS_FE.Pages.Staff.AirlinesManagement.AirplaneManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<AirplaneResponseModel> Airplane { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
           
            var response = await APIHelper.GetAsJsonAsync<List<AirplaneResponseModel>>(client, "airplane/get-all-airplane");
            if (response != null)
            {
                Airplane = PaginatedList<AirplaneResponseModel>.Create(response, pageIndex ?? 1, 6);
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
