using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RefundBankAccount
{
    public string Id { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;
}
