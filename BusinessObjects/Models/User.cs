using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Avatar { get; set; }

    public string Name { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public int? Point { get; set; }

    public string? RankId { get; set; }

    public string Role { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual Rank? Rank { get; set; }

    public virtual ICollection<RefundTransaction> RefundTransactions { get; set; } = new List<RefundTransaction>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
