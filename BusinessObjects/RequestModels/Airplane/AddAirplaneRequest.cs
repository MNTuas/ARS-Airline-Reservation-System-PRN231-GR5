using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Airplane
{
    public class AddAirplaneRequest
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(50, ErrorMessage = "Code cannot be longer than 10 characters.")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "AvailableSeat must be greater than 0.")]
        public int AvailableSeat { get; set; }

        public string? AirlinesId { get; set; }

        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be either 'Active' or 'Inactive'.")]
        public string? Status { get; set; }
    }
}
