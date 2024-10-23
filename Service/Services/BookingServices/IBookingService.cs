using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.BookingServices
{
    public interface IBookingService
    {
        Task<Result<BookingInformation>> addBooking(CreateBookingRequest createBookingRequest);
    }
}
