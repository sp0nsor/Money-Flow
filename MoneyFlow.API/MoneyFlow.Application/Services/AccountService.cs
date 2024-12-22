using Microsoft.AspNetCore.Http;
using MoneyFlow.AppLication.Contracts;
using MoneyFlow.Core.Models;
using MoneyFlow.DataAccess.Repositories;
using MoneyFlow.Infrastructure;

namespace MoneyFlow.AppLication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IKeyHasher keyHasher;

        public AccountService(IAccountRepository accountRepository, IKeyHasher keyHasher)
        {
            this.accountRepository = accountRepository;
            this.keyHasher = keyHasher;
        }

        public async Task<IResult> CreateAccount(AccountRequest request)
        {
            var keyHash = keyHasher.Generate(request.ScretKey);

            var (account, error) = Account.Create(
                Guid.NewGuid(),
                keyHash,
                request.FirstName,
                request.LastName,
                request.MoneyAmount);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var result = await accountRepository.Create(account);

            return Results.Ok(result);
        }

        public async Task<IResult> GetAccounts()
        {
            var accounts = await accountRepository.Get();

            var response = accounts.Select(a =>
                new AccountResponse(
                    a.AccountNumber,
                    a.OwnerFirstName,
                    a.OwnerLastName,
                    a.MoneyAmount));

            return Results.Ok(response);
        }

        public async Task<IResult> MakeTranfer(TransferRequest request)
        {
            var account = await accountRepository
                .GetByAccountNumber(request.SenderAccountNumber);

            var isTrueSender = keyHasher.Verify(
                request.SenderSecretKey,
                account.SecretKeyHash);

            if (!isTrueSender)
            {
                return Results.BadRequest("Account data has not been verified");
            }

            if (request.BookPrice > account.MoneyAmount)
            {
                return Results.BadRequest("There is not enough money in the account");
            }

            await accountRepository.MakeTransfer(
                request.SenderAccountNumber,
                request.recipientAccountNumber,
                request.BookPrice);

            return Results.Ok();
        }
    }
}
