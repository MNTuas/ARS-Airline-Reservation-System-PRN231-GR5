using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Passenger
{
    public class UpdatePassengerRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "First name cannot contain numbers or special characters")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Last name cannot contain numbers or special characters")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Gender is required")] 
        public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "Date of birth is required")]
        public DateOnly Dob { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = null!;
    }
}
