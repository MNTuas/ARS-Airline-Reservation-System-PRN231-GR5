using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.User
{
    public class UserInfoUpdateModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "First name cannot contain numbers or special characters")]
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string Email { get; set; } = null!;

        public string? Address { get; set; }
    }
}
