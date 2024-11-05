using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Airplane
{
    public class AddAirplaneRequest
    {
        [Required(ErrorMessage = "Please enter airplane code number.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Code number must start with a non-zero digit and contain only numeric characters.")]
        public string CodeNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please choose airlines.")]
        public string AirlinesId { get; set; } = null!;

        [Required(ErrorMessage = "Please enter amount of seat for each class.")]
        public virtual List<AirplaneSeatRequest> AirplaneSeatRequest { get; set; } = new List<AirplaneSeatRequest>();
    }

    public class AirplaneSeatRequest
    {
        [Required(ErrorMessage = "Please choose seat class.")]
        public string SeatClassId { get; set; } = null!;

        [Required(ErrorMessage = "Please enter seat count.")]
        public int SeatCount { get; set; }
    }
}
