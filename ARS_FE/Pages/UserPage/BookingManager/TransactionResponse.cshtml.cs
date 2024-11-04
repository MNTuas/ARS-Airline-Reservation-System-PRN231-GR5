using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Enums;
using System.Net.Http.Headers;
using System.Net.Http;
using BusinessObjects.ResponseModels.VnPay;
using BusinessObjects.Models;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class TransactionResponse : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionResponse(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Query.Count == 0)
            {
                TempData["Error"] = "Error making payment, please try again later!";
                return Page();
            }
            var client = CreateAuthorizedClient();

            var response = new VnPaymentResponseModel
            {
                OrderDescription = Request.Query["vnp_OrderInfo"].ToString(),
                OrderId = Request.Query["vnp_OrderInfo"].ToString().Split(":")[1].Trim(),
                PaymentId = Request.Query["vnp_TxnRef"].ToString(),
                TransactionId = Request.Query["vnp_TransactionNo"].ToString(),
                Token = Request.Query["vnp_SecureHash"].ToString(),
                VnPayResponseCode = Request.Query["vnp_ResponseCode"].ToString(),
                Success = true
            };

            if (response.VnPayResponseCode.Equals("00"))
            {
                var transactionUpdate = await APIHelper.PutAsJson<string>(client, $"Transaction/{response.PaymentId}", "Paid");
                if (!transactionUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update transaction status");
                }

                var bookingUpdate = await APIHelper.PutAsJson<string>(client, $"Booking/{response.OrderId}", "Paid");
                if (!bookingUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update booking status");
                }
            }
            else
            {
                var transactionUpdate = await APIHelper.PutAsJson<string>(client, $"Transaction/{response.PaymentId}", "Cancelled");
                if (!transactionUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update transaction status");
                }
                var bookingUpdate = await APIHelper.PutAsJson<string>(client, $"Booking/{response.OrderId}", "Cancelled");
                if (!bookingUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update booking status");
                }

            }

            return RedirectToPage("./BookingList");
        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return client;
            }

            return null;
        }


    }
}

