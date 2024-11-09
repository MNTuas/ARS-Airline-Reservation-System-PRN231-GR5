using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.RefundBankAccount
{
    public class RefundBankAccountResponseModel
    {
        public string Id { get; set; } = null!;

        public string AccountName { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public string BankName { get; set; } = null!;

        public string BookingId { get; set; } = null!;
    }
}
