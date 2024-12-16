namespace MoneyFlow.AppLication.Contracts
{
    public record TransferRequest(
        Guid SenderAccountNumber,
        string SenderSecretKey,
        Guid RecipientAccountNumber,
        decimal moneyAmount);
}
