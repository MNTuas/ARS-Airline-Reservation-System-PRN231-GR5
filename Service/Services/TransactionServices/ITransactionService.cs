using BusinessObjects.ResponseModels.Transaction;
using Microsoft.AspNetCore.Http;

namespace Service.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<List<TransactionResponseModel>> GetTransactionByUserId(string userId);
        Task<string> CreateTransaction(string bookingId, string token, HttpContext httpContext);
        Task UpdateTransactionStatus(string id, string status);
        Task<bool> SendEmailWhenBuySucces(string bookingId, string flightId);
    }
}
