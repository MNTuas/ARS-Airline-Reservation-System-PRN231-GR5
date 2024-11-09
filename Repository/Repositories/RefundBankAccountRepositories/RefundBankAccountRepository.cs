using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.RefundBankAccountRepositories
{
    public class RefundBankAccountRepository : GenericDAO<RefundBankAccount>, IRefundBankAccountRepository
    {
        public async Task<RefundBankAccount> GetRefundBankAccountByBookingId(string bookingId)
        {
            return await GetSingle(r => r.BookingId.Equals(bookingId));
        }
    }
}
