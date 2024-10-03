using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Airport
{
    public class UpdateAirportRequest
    {
        public string? Name { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

    }
}
