using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.RefundTransactionRepositories
{
    public interface IRefundTransactionRepository : IGenericRepository<RefundTransaction>
    {
        Task<RefundTransaction> GetRefundTransactionById(string id);
        Task<List<RefundTransaction>> GetAllPendingRefundTransaction();
    }
}
