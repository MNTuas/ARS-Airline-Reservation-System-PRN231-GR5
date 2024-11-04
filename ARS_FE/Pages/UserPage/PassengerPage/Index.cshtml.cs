using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using System.Net.Http.Headers;
using System.Net.Http;
using BusinessObjects.ResponseModels.Airport;
using Service;
using BusinessObjects.ResponseModels.Passenger;

namespace ARS_FE.Pages.UserPage.PassengerPage
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<PassengerResposeModel> PassengerRespose { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<PassengerResposeModel>>(client, "Passenger/get-all");
            if (response != null)
            {
                PassengerRespose = response;
            }
            else
            {
                PassengerRespose = new List<PassengerResposeModel>();
            }
            return Page();
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
