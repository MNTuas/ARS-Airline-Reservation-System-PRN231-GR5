using System.ComponentModel.DataAnnotations;

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
