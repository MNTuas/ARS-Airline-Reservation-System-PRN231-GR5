using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Auth
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Full name can only contain letters and spacssses.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
