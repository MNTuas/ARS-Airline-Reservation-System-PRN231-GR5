using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Passenger
{
    public class CreatePassengerRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        //public DateOnly Dob { get; set; }

        public string Country { get; set; } = null!;

        public string Type { get; set; } = null!;

    }
}
