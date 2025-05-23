﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingInformation
{
    public string Id { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int Quantity { get; set; }

    public string UserId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool? IsRefund { get; set; }

    public DateTime? CancelDate { get; set; }

    public virtual ICollection<RefundBankAccount> RefundBankAccounts { get; set; } = new List<RefundBankAccount>();

    public virtual ICollection<RefundTransaction> RefundTransactions { get; set; } = new List<RefundTransaction>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
