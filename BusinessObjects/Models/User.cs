using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Avatar { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Point { get; set; }

    public string RankId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<PaymentRecord> PaymentRecords { get; set; } = new List<PaymentRecord>();

    public virtual Rank Rank { get; set; } = null!;

    public virtual ICollection<Relative> Relatives { get; set; } = new List<Relative>();
}
