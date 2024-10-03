using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingInformation
{
    public string Id { get; set; } = null!;

    public string? FlightId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? FlightClassId { get; set; }

    public string? UserId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Flight? Flight { get; set; }

    public virtual FlightClass? FlightClass { get; set; }

    public virtual ICollection<PassengerOfBooking> PassengerOfBookings { get; set; } = new List<PassengerOfBooking>();

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual User? User { get; set; }
}
