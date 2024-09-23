using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Relative
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string PassengerId { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
