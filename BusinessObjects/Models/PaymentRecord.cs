using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class PaymentRecord
{
    public string Id { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public decimal Price { get; set; }

    public double? Discount { get; set; }

    public decimal FinalPrice { get; set; }

    public DateOnly CreatedDate { get; set; }

    public DateOnly? PayDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
