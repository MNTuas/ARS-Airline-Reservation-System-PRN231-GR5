using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.TransactionRepositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<List<Transaction>> GetTransactionByUserId(string userId);

        Task<Transaction> GetTransactionById(string id);
    }
}
