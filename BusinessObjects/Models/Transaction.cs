using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Transaction
{
    public string Id { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public decimal FinalPrice { get; set; }

    public DateTime PayDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
