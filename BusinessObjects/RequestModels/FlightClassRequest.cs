using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels
{
    public class FlightClassRequest
    {
        [Required(ErrorMessage = "FlightId is required.")]
        public string FlightId { get; set; }
        [Required(ErrorMessage = "Class is required.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Class can not contain numbers or special characters")]
        public string Class { get; set; } 
        [Required(ErrorMessage = "Quantity is required.")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Quantity is positive integers only")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"^(?!0*\.?0+$)([1-9]\d*|0)(\.\d+)?$", ErrorMessage = "Price only accept positive integers and positive real numbers")]
        public decimal Price { get; set; }
    }
}
