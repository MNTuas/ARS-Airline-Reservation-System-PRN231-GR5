using BusinessObjects.Models;
using BusinessObjects.RequestModels.VnPay;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.TransactionRepositories;
using Service.Enums;
using Service.Services.VNPayServices;
using Service.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IVnPayService _vnPayService;

        public TransactionService(ITransactionRepository transactionRepository, IBookingRepository bookingRepository, IVnPayService vnPayService)
        {
            _transactionRepository = transactionRepository;
            _bookingRepository = bookingRepository;
            _vnPayService = vnPayService;
        }

        public async Task<string> CreateTransaction(string bookingId, string token, HttpContext httpContext)
        {
            var userId = JwtDecode.DecodeTokens(token, "UserId");
            var totalPrice = await _bookingRepository.GetTotalPriceOfBooking(bookingId);
            Transaction newTransaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = bookingId,
                FinalPrice = totalPrice,
                CreatedDate = DateTime.Now,
                UserId = userId,
                Status = BookingStatusEnums.Pending.ToString()
            };

            VnPaymentRequestModel vnPayment = new VnPaymentRequestModel
            {
                OrderId = bookingId,
                Amount = totalPrice,
                CreatedDate = DateTime.Now,
                PaymentId = newTransaction.Id,
                RedirectUrl = "https://localhost:7223/UserPage/BookingManager/TransactionResponse"
            };

            await _transactionRepository.Insert(newTransaction);
            string checkOutUrl = _vnPayService.CreatePaymentUrl(httpContext, vnPayment);
            return checkOutUrl;
        }

        public async Task UpdateTransactionStatus(string id, string status)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null)
            {
                throw new Exception("Not found!");
            }
            transaction.Status = status;
            await _transactionRepository.Update(transaction);
        }
    }
}
