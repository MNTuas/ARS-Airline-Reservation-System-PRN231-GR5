using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Transaction;
using Repository.Repositories.TransactionReposotories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository,IMapper mapper)
        {
            this._transactionRepository = transactionRepository;
            this._mapper = mapper;
        }
        public async Task<TransactionResponseModel> GetTransactionByUserId(string userId)
        {
            var tran = await _transactionRepository.GetTransactionByUserId(userId);
            return _mapper.Map<TransactionResponseModel>(tran);
        }
    }
}
