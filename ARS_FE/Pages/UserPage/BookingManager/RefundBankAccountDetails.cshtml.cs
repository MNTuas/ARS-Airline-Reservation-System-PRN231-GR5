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
using BusinessObjects.ResponseModels.RefundBankAccount;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class RefundBankAccountDetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RefundBankAccountDetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public RefundBankAccountResponseModel RefundBankAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<RefundBankAccountResponseModel>(client, $"refund-bank-account/{id}");

            if (response != null)
            {
                RefundBankAccount = response;
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
