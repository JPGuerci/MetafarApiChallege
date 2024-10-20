﻿using AutoMapper;
using MetafarApiChallege.Infrastructure.Dtos;
using MetafarApiChallege.Infrastructure.Helpers;
using MetafarApiChallege.Infrastructure.Repositories.Interfaces;
using MetafarApiChallege.Infrastructure.Repositories.Models;
using MetafarApiChallege.Infrastructure.Services.Interfaces;
using System.Net;

namespace MetafarApiChallege.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IOperationService _operationService;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IAccountRepository accountRepository, IOperationService operationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _operationService = operationService;
        }

        public async Task<AccountDto> GetByIdCard(Guid idCard)
        {
            Account? account = await GetAndValidate(idCard);
            AccountDto response = _mapper.Map<AccountDto>(account);
            OperationDto operation = await _operationService.GetLast(account.Id, TransactionTypesHelper.CashOut);
            if (operation != null) response.LastExtraction = operation.Date;

            await _operationService.AddOperation(account, 0, idCard, TransactionTypesHelper.Inquiry);
            await _unitOfWork.CommitAsync();

            return response;
        }

        public async Task<OperationDto> CashIn(Guid idCard, decimal amount)
        {
            Account? account = await GetAndValidate(idCard);
            OperationDto result = await _operationService.AddOperation(account, amount, idCard, TransactionTypesHelper.CashIn);
            await UpdateBalance(account, amount);

            await _unitOfWork.CommitAsync();

            return result;
        }

        public async Task<OperationDto> CashOut(Guid idCard, decimal amount)
        {
            Account? account = await GetAndValidate(idCard);
            decimal amountNeg = -Math.Abs(amount);
            if ((account.CurrentBalance + amountNeg) < 0)
                throw new BusinessException("Insufficient funds for this transaction.");

            OperationDto result = await _operationService.AddOperation(account, amountNeg, idCard, TransactionTypesHelper.CashOut);
            await UpdateBalance(account, amountNeg);

            await _unitOfWork.CommitAsync();

            return result;
        }

        public async Task<TransferResponse> Transfer(TransferDto transfer)
        {
            Account? accountOrigin = await GetAndValidate(transfer.IdCardOrigin);
            Account? accountDestiny = await GetAndValidate(transfer.CardNumberDestiny);
            decimal amount = -Math.Abs(transfer.Amount);

            if (accountOrigin.Id.Equals(accountDestiny.Id))
                throw new BusinessException("The cards belong to the same account.");

            if (accountOrigin.CurrentBalance + amount < 0)
                throw new BusinessException("Insufficient funds for this transaction.");

            OperationDto operationOrigin = await _operationService.AddOperation(accountOrigin, amount, transfer.IdCardOrigin, TransactionTypesHelper.Transfer);
            await UpdateBalance(accountOrigin, amount);

            OperationDto operationDestiny = await _operationService.AddOperation(accountDestiny, transfer.Amount, transfer.IdCardOrigin, TransactionTypesHelper.Transfer);
            await UpdateBalance(accountDestiny, transfer.Amount);

            await _unitOfWork.CommitAsync();

            TransferResponse result = _mapper.Map<TransferResponse>(operationOrigin);
            result.CardNumberDestiny = transfer.CardNumberDestiny;
            result.Balance = accountOrigin.CurrentBalance;

            return result;
        }

        private async Task<Account> GetAndValidate(Guid idCard)
        {
            Account? account = await _accountRepository.GetByIdCard(idCard);
            if (account == null) throw new AccountNotFoundException("Account not found for the given IdCard.");
            return account;
        }

        private async Task<Account> GetAndValidate(int cardNumber)
        {
            Account? account = await _accountRepository.GetByCardNumber(cardNumber);
            if (account == null) throw new AccountNotFoundException("Account not found for the given Card Number.");
            return account;
        }

        private async Task UpdateBalance(Account account, decimal amount)
        {
            account.CurrentBalance += amount;
            _accountRepository.Update(account);
            await Task.CompletedTask;
        }
    }
}
