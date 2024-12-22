﻿namespace MoneyFlow.AppLication.Contracts
{
    public record TransferRequest(
        Guid SenderAccountNumber,
        string SenderSecretKey,
        decimal BookPrice,
        Guid recipientAccountNumber);
}
