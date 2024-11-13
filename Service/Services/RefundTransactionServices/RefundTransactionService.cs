using BusinessObjects.Models;
using BusinessObjects.RequestModels.VnPay;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.RefundTransactionRepositories;
using Repository.Repositories.UserRepositories;
using Service.Enums;
using Service.Services.FlightServices;
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
        private readonly IFlightRepository _flightRepository;

        public RefundTransactionService(IRefundTransactionRepository refundTransactionRepository, IUserRepository userRepository, IBookingRepository bookingRepository, IVnPayService vnPayService, IFlightRepository flightRepository)
        {
            _refundTransactionRepository = refundTransactionRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _vnPayService = vnPayService;
            _flightRepository = flightRepository;
        }

        public async Task<string> RefundBookingTransaction(string bookingId, string token, HttpContext httpContext)
        {
            var staffId = JwtDecode.DecodeTokens(token, "UserId");
            var booking = await _bookingRepository.GetById(bookingId);
            var user = await _userRepository.GetUserById(booking.UserId);
            var totalPrice = await _bookingRepository.GetTotalPriceOfBooking(bookingId);
            var flightId = booking.Tickets.FirstOrDefault().TicketClass.FlightId;
            var flight = await _flightRepository.GetFlightById(flightId);
            var distanceToFlight = flight.DepartureTime.Subtract(booking.CancelDate.Value).TotalDays;
            var refundPercent = 100;

            if (distanceToFlight < 7 && distanceToFlight > 1)
            {
                refundPercent = 90;
            }
            else if (distanceToFlight < 1 && distanceToFlight > 0)
            {
                refundPercent = 70;
            }

            RefundTransaction newTransaction = new RefundTransaction
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = bookingId,
                RefundAmount = (totalPrice * (100 - user.Rank.Discount) / 100) * refundPercent / 100,
                CreatedDate = DateTime.Now,
                RefundBy = staffId,
                Status = BookingStatusEnums.Pending.ToString()
            };

            VnPaymentRequestModel vnPayment = new VnPaymentRequestModel
            {
                OrderId = bookingId,
                Amount = (totalPrice * (100 - user.Rank.Discount) / 100) * refundPercent / 100,
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
