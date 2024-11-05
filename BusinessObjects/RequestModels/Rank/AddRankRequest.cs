using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Rank
{
    public class AddRankRequest
    {
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal Discount { get; set; }
    }
}
