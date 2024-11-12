using BusinessObjects.Models;
using BusinessObjects.RequestModels.VnPay;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.RefundTransactionRepositories;
using Repository.Repositories.UserRepositories;
using Service.Enums;
using Service.Services.VNPayServices;
using Service.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RefundTransactionServices
{
    public class RefundTransactionService : IRefundTransactionService
    {
        private readonly IRefundTransactionRepository _refundTransactionRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVnPayService _vnPayService;

        public RefundTransactionService(IRefundTransactionRepository refundTransactionRepository, IUserRepository userRepository, IBookingRepository bookingRepository, IVnPayService vnPayService)
        {
            _refundTransactionRepository = refundTransactionRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _vnPayService = vnPayService;
        }

        public async Task<string> RefundBookingTransaction(string bookingId, string token, HttpContext httpContext)
        {
            var staffId = JwtDecode.DecodeTokens(token, "UserId");
            var booking = await _bookingRepository.GetById(bookingId);
            var user = await _userRepository.GetUserById(booking.UserId);
            var totalPrice = await _bookingRepository.GetTotalPriceOfBooking(bookingId);
            RefundTransaction newTransaction = new RefundTransaction
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = bookingId,
                RefundAmount = totalPrice * (100 - user.Rank.Discount) / 100,
                CreatedDate = DateTime.Now,
                RefundBy = staffId,
                Status = BookingStatusEnums.Pending.ToString()
            };

            VnPaymentRequestModel vnPayment = new VnPaymentRequestModel
            {
                OrderId = bookingId,
                Amount = totalPrice * (100 - user.Rank.Discount) / 100,
                CreatedDate = DateTime.Now,
                PaymentId = newTransaction.Id,
                RedirectUrl = "https://localhost:7223/Staff/CancelBookingManagement/RefundTransactionResponse"
            };

            await _refundTransactionRepository.Insert(newTransaction);

            string checkOutUrl = _vnPayService.CreatePaymentUrl(httpContext, vnPayment);
            return checkOutUrl;
        }

        public async Task UpdateRefundTransactionStatus(string id, string status)
        {
            var transaction = await _refundTransactionRepository.GetRefundTransactionById(id);
            if (transaction == null)
            {
                throw new Exception("Not found!");
            }
            transaction.Status = status;
            await _refundTransactionRepository.Update(transaction);
        }
    }
}
