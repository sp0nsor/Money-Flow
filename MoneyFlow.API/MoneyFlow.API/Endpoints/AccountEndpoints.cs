using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.AppLication.Contracts;
using MoneyFlow.AppLication.Services;

namespace MoneyFlow.API.Endpoints
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("api/account");

            group.MapGet("/info", GetAccounts);
            group.MapPost("/create", CreateAccount);
            group.MapPut("/transfer", MakeTransfer);

            return builder;
        }

        private static async Task<IResult> CreateAccount(
            [FromBody] AccountRequest request,
            IAccountService accountService)
        {
            Console.WriteLine(request.MoneyAmount);
            var response = await accountService.CreateAccount(request);

            return response;
        }

        private static async Task<IResult> GetAccounts(IAccountService accountService)
        {
            var response = await accountService.GetAccounts();

            return response;
        }

        private static async Task<IResult> MakeTransfer(
            [FromBody] TransferRequest request,
            IAccountService accountService)
        {
            var response = await accountService.MakeTranfer(request);

            return response;
        }
    }
}
