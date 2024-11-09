using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.RefundBankAccount;
using BusinessObjects.ResponseModels.RefundBankAccount;
using Repository.Repositories.RefundBankAccountRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RefundBankAccountServices
{
    public class RefundBankAccountService : IRefundBankAccountService
    {
        private readonly IRefundBankAccountRepository _refundBankAccountRepository;
        private readonly IMapper _mapper;

        public RefundBankAccountService(IRefundBankAccountRepository refundBankAccountRepository, IMapper mapper)
        {
            _refundBankAccountRepository = refundBankAccountRepository;
            _mapper = mapper;
        }

        public async Task<RefundBankAccountResponseModel> GetRefundBankingAccountByBookingId(string bookingId)
        {
            var account = await _refundBankAccountRepository.GetRefundBankAccountByBookingId(bookingId);
            return _mapper.Map<RefundBankAccountResponseModel>(account);
        }

        public async Task CreateRefundBankAccount(RefundBankAccountCreateModel model)
        {
            RefundBankAccount refundBankAccount = _mapper.Map<RefundBankAccount>(model);
            await _refundBankAccountRepository.Insert(refundBankAccount);
        }

        public async Task UpdateRefundBankAccount(RefundBankAccountUpdateModel model, string bookingId)
        {
            var bankAccount = await _refundBankAccountRepository.GetRefundBankAccountByBookingId(bookingId);
            if (bankAccount == null)
            {
                throw new Exception("Account not found!");
            }
            _mapper.Map(model, bankAccount);
            await _refundBankAccountRepository.Update(bankAccount);
        }
    }
}
