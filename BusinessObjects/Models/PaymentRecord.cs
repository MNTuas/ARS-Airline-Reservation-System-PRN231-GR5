using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class PaymentRecord
{
    public string Id { get; set; } = null!;

    public string? BookingId { get; set; }

    public string? UserId { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal FinalPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? PayDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual BookingInformation? Booking { get; set; }

    public virtual User? User { get; set; }
}
