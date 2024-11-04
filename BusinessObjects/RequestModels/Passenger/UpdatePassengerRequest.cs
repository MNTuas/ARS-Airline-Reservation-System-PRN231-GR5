using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Passenger
{
    public class UpdatePassengerRequest
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public DateOnly Dob { get; set; }
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
    }
}
