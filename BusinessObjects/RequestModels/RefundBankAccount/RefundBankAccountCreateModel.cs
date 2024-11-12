using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.RefundBankAccount
{
    public class RefundBankAccountCreateModel
    {
        [Required(ErrorMessage = "Please enter account name")]
        public string AccountName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter account number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Account number must contain only numbers")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please enter bank name")]
        public string BankName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter booking id")]
        public string BookingId { get; set; } = null!;
    }
}
