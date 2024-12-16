using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MoneyFlow.AppLication.Contracts
{
    public record AccountResponse(
        Guid AccountNumber,
        string FirstName,
        string LastName,
        decimal MoneyAmount);
}
