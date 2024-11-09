using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.RefundBankAccount
{
    public class RefundBankAccountUpdateModel
    {
        public string? UpdateId { get; set; }

        [Required(ErrorMessage = "Please enter account name")]
        public string AccountName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter account number")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please enter bank name")]
        public string BankName { get; set; } = null!;
    }
}
