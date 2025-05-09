namespace SimpleWallet.Application.Exceptions;

using System;

[Serializable]
public class InsufficientFundsException : Exception
{
    public decimal AttemptedDebitAmount { get; private set; }
    public decimal CurrentBalance { get; private set; }

    public InsufficientFundsException()
    {
    }

    public InsufficientFundsException(string message, decimal attemptedDebitAmount, decimal currentBalance)
        : base(message)
    {
        AttemptedDebitAmount = attemptedDebitAmount;
        CurrentBalance = currentBalance;
    }

    public InsufficientFundsException(string message, decimal attemptedDebitAmount, decimal currentBalance, Exception innerException)
        : base(message, innerException)
    {
        AttemptedDebitAmount = attemptedDebitAmount;
        CurrentBalance = currentBalance;
    }
}
