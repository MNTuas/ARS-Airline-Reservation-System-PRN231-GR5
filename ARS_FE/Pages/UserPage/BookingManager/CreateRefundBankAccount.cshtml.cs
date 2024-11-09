using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Airport;
using System.Net.Http.Headers;
using BusinessObjects.RequestModels.RefundBankAccount;
using BusinessObjects.ResponseModels.Booking;
using Service;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class CreateRefundBankAccountModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateRefundBankAccountModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RefundBankAccountCreateModel RefundBankAccount { get; set; } = new RefundBankAccountCreateModel();

        public void OnGet(string bookingId)
        {
            RefundBankAccount.BookingId = bookingId; // Set directly on RefundBankAccount
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.PostAsJson(client, "refund-bank-account", RefundBankAccount);

            if (response.IsSuccessStatusCode)
            {
                var responseCancel = await APIHelper.PutAsJson(client, "Booking/cancel", RefundBankAccount.BookingId);

                if (responseCancel.IsSuccessStatusCode)
                {
                    return RedirectToPage("./BookingList");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to cancel the booking. Please try again.";
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the refund bank account.");
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
