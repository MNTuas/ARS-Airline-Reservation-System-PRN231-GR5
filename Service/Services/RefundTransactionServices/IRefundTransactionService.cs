using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RefundTransactionServices
{
    public interface IRefundTransactionService
    {
        Task<string> RefundBookingTransaction(string bookingId, string token, HttpContext httpContext);

        Task UpdateRefundTransactionStatus(string id, string status);

        Task<string> AutoUpdateRefundTransactionStatus();
    }
}
