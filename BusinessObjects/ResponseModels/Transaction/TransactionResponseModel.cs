using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.Transaction
{
    public class TransactionResponseModel
    {
        public decimal FinalPrice { get; set; }

        public DateTime PayDate { get; set; }

        public string Status { get; set; } = null!;

        public virtual BookingInformation Booking { get; set; } = null!;

        public virtual Models.User User { get; set; } = null!;
    }
}
