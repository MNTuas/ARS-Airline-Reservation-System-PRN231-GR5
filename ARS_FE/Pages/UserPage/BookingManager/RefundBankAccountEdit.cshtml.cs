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
using BusinessObjects.RequestModels.Airport;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.RefundBankAccount;
using BusinessObjects.RequestModels.RefundBankAccount;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class RefundBankAccountEditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RefundBankAccountEditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RefundBankAccountUpdateModel RefundBankAccount { get; set; } = default!;

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
                RefundBankAccount = new RefundBankAccountUpdateModel
                {
                    AccountName = response.AccountName,
                    AccountNumber = response.AccountNumber,
                    BankName = response.BankName,
                    UpdateId = id,
                };
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var client = CreateAuthorizedClient();
            var bookingId = RefundBankAccount.UpdateId;

            var response = await APIHelper.PutAsJson(client, $"refund-bank-account/{bookingId}", RefundBankAccount);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./BookingList");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while update the airline.");
                return Page();
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
