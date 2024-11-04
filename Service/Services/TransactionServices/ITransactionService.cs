using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<string> CreateTransaction(string bookingId, string token, HttpContext httpContext);
        Task UpdateTransactionStatus(string id, string status);
        Task<bool> SendEmailWhenBuySucces(string bookingId, string flightId);
    }
}
