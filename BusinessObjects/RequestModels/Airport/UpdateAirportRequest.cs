using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Airport
{
    public class UpdateAirportRequest
    {
        [Required(ErrorMessage = "Please enter airport name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter airport city")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Please enter airport country")]
        public string Country { get; set; } = null!;

    }
}
