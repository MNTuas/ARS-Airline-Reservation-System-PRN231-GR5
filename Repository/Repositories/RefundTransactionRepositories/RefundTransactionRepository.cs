using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.RefundTransactionRepositories
{
    public class RefundTransactionRepository : GenericDAO<RefundTransaction>, IRefundTransactionRepository
    {
        public async Task<RefundTransaction> GetRefundTransactionById(string id)
        {
            return await GetSingle(t => t.Id.Equals(id));
        }
    }
}
