using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RefundTransaction
{
    public string Id { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public decimal RefundAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string RefundBy { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual User RefundByNavigation { get; set; } = null!;
}
