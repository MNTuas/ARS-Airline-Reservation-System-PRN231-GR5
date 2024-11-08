namespace BusinessObjects.Models;

public partial class Rank
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Discount { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
