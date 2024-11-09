using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.RefundBankAccountRepositories
{
    public interface IRefundBankAccountRepository : IGenericRepository<RefundBankAccount>
    {
        Task<RefundBankAccount> GetRefundBankAccountByBookingId(string bookingId);
    }
}
