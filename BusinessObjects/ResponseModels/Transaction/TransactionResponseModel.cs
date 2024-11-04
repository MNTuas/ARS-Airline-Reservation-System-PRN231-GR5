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
        public string Id { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public decimal FinalPrice { get; set; }

        public DateTime? PayDate { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public virtual BookingInformationResponseModel Booking { get; set; } = null!;

    }
    public class BookingInformationResponseModel
    {
        public string Id { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; } = null!;

        public string Status { get; set; } = null!;

    }
}
