using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TransactionService
{
    public interface ITransactionService 
    {
        Task<TransactionResponseModel> GetTransactionByUserId(string userId);
    }
}
