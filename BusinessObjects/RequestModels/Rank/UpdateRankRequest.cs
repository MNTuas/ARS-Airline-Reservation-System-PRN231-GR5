using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Rank
{
    public class UpdateRankRequest
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal Discount { get; set; }
    }
}
