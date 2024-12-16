using Microsoft.AspNetCore.Http;
using MoneyFlow.AppLication.Contracts;

namespace MoneyFlow.AppLication.Services
{
    public interface IAccountService
    {
        Task<IResult> CreateAccount(AccountRequest request);
        Task<IResult> GetAccounts();
        Task<IResult> MakeTranfer(TransferRequest request);
    }
}