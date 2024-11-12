using BusinessObjects.ResponseModels.VnPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.CancelBookingManagement
{
    public class RefundTransactionResponseModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RefundTransactionResponseModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var bookingId = HttpContext.Session.GetString("BookingId");
            if (string.IsNullOrEmpty(bookingId))
            {
                TempData["Error"] = "Không tìm thấy Booking ID. Vui lòng thử lại.";
                return Page();
            }

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
                var transactionUpdate = await APIHelper.PutAsJson<string>(client, $"refund-transaction/{response.PaymentId}", "Paid");
                if (!transactionUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update transaction status");
                }
                var bookingUpdate = await APIHelper.PutAsJson<string>(client, $"Booking/refund", $"{response.OrderId}");
                if (!bookingUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update booking status");
                }
            }
            else
            {
                var transactionUpdate = await APIHelper.PutAsJson<string>(client, $"refund-transaction/{response.PaymentId}", "Cancelled");
                if (!transactionUpdate.IsSuccessStatusCode)
                {
                    throw new Exception("Error in update transaction status");
                }
            }
            return RedirectToPage("./Index");

            //var sendEmail = await APIHelper.PostSendEmail(client, $"Transaction/SendEmailSuccess/{bookingId}?flightId={flightId}");
            //if (sendEmail.IsSuccessStatusCode)
            //{
            //    // Sau khi gửi email thành công, lưu thông báo vào TempData và chuyển hướng đến trang thanh toán thành công
            //    TempData["SuccessMessage"] = "Payment Successful! Redirecting...";
            //    return RedirectToPage("./PaymentSuccess", new { bookingId = bookingId });
            //}
            //else
            //{
            //    // Nếu gửi email không thành công, hiển thị thông báo lỗi
            //    TempData["ErrorMessage"] = "Payment Failed!";
            //    return RedirectToPage("./BookingList");
            //}
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
