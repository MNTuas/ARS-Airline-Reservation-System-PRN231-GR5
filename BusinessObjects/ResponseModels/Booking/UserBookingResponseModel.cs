﻿using BusinessObjects.ResponseModels.Ticket;

namespace BusinessObjects.ResponseModels.Booking
{
    public class UserBookingResponseModel
    {
        public string Id { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public decimal Discount { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; } = null!;

        public string FlightId { get; set; } = null!;

        public string FlightStatus { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public bool? IsRefund { get; set; }

        public DateTime? CancelDate { get; set; }

        public virtual List<TicketResponseModel> Tickets { get; set; } = new List<TicketResponseModel>();

    }


}
