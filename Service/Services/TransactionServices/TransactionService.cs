using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.VnPay;
using BusinessObjects.ResponseModels.Booking;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.TransactionRepositories;
using Repository.Repositories.UserRepositories;
using Service.Enums;
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

        public TransactionService(ITransactionRepository transactionRepository, IBookingRepository bookingRepository,
                                  IVnPayService vnPayService, IUserRepository userRepository,
                                  IEmailService emailService, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _bookingRepository = bookingRepository;
            _vnPayService = vnPayService;
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
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

            // Gửi email thông báo
            await SendEmailWhenBuySucces(userId, bookingId); // Thêm hàm gửi email tại đây

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

        public async Task SendEmailWhenBuySucces(string userId, string bookingId)
        {
           
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

          
            var booking = await _bookingRepository.GetById(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found!");
            }

            // Ánh xạ booking sang UserBookingResponseModel
            var userBookingResponse = _mapper.Map<UserBookingResponseModel>(booking);

            
            var tickets = userBookingResponse.Tickets;

            
            var htmlBody = EmailTemplate.ListTicket(user.Name, user.Email, tickets);

            // Gửi email
            bool sendEmailSuccess = await _emailService.SendEmail(user.Email, "Your Booking Confirmation", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new Exception("Error in sending email");
            }
        }


    }
}
