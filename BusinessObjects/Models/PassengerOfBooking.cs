using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class PassengerOfBooking
{
    public string Id { get; set; } = null!;

    public string PassengerId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;
}
