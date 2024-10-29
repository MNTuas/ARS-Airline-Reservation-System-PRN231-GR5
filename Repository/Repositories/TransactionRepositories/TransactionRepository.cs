using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.TransactionRepositories
{
    public class TransactionRepository : GenericDAO<Transaction>, ITransactionRepository
    {
        public async Task<Transaction> GetTransactionById(string id)
        {
            return await GetSingle(t => t.Id.Equals(id));
        }
        public async Task<Transaction> GetTransactionByUserId(string userId)
        {
            var tranList = await GetSingle(t => t.UserId == userId, includeProperties: "Booking");
            return tranList;
        }
    }
}
