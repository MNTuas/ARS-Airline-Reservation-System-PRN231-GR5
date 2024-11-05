using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Airlines
{
    public class AirlinesCreateModel
    {
        [Required(ErrorMessage = "Please enter airlines name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter airlines code")]
        public string Code { get; set; } = null!;
    }
}
