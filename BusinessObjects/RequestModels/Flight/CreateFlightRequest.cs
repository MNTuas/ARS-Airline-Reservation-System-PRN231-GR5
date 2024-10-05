using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Flight
{
    public class CreateFlightRequest
    {
        [Required(ErrorMessage = "Please choose airlines")]
        public string AirlinesId { get; set; } = null!;
        
        [Required(ErrorMessage = "Please choose airplane")]
        public string AirplaneId { get; set; } = null!;

        [Required(ErrorMessage = "Please choose departure time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Please choose arrival time")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "Please choose origin")]
        public string From { get; set; } = null!;

        [Required(ErrorMessage = "Please choose destination")]
        public string To { get; set; } = null!;
    }
}
