using BusinessObjects.RequestModels.RefundBankAccount;
using BusinessObjects.ResponseModels.RefundBankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RefundBankAccountServices
{
    public interface IRefundBankAccountService
    {
        Task<RefundBankAccountResponseModel> GetRefundBankingAccountByBookingId(string bookingId);
        Task CreateRefundBankAccount(RefundBankAccountCreateModel model);
        Task UpdateRefundBankAccount(RefundBankAccountUpdateModel model, string bookingId);
    }
}
