using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.VnPay;
using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Flight;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.TransactionRepositories;
using Repository.Repositories.UserRepositories;
using Service.Enums;
using Service.Helper;
using Service.Services.EmailServices;
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
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFlightRepository _flightRepository;


        public TransactionService(ITransactionRepository transactionRepository, IBookingRepository bookingRepository,
                                  IVnPayService vnPayService, IUserRepository userRepository,
                                  IEmailService emailService, IMapper mapper,
                                  IHttpContextAccessor httpContextAccessor, IFlightRepository flightRepository)
        {
            _transactionRepository = transactionRepository;
            _bookingRepository = bookingRepository;
            _vnPayService = vnPayService;
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _flightRepository = flightRepository;
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

        public async Task<bool> SendEmailWhenBuySucces(string bookingId, string flightId)
        {
            var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
            var userid = idclaim?.Value;

            var user = await _userRepository.GetUserById(userid);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            var booking = await _bookingRepository.GetById(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found!");
            }

            var flight = await _flightRepository.GetFlightById(flightId);
            if (flight == null)
            {
                throw new Exception("Flight not found!");
            }


            // Ánh xạ booking sang UserBookingResponseModel
            var userBookingResponse = _mapper.Map<UserBookingResponseModel>(booking);
            var flightResponse = _mapper.Map<FlightResponseModel>(flight);
            var tickets = userBookingResponse.Tickets;

            var htmlBody = EmailTemplate.ListTicket(user.Name, user.Email, tickets, flightResponse);

            // Gửi email
            bool sendEmailSuccess = await _emailService.SendEmail(user.Email, "Your Booking Confirmation", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new Exception("Error in sending email");
            }

            return true; // Trả về true nếu email gửi thành công
        }



    }
}
