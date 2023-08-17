namespace Domain.Exceptions;

public class AccountInsufficientFundsException : Exception
{
    public AccountInsufficientFundsException(decimal balance) 
        : base($"Insufficient Funds In Account. Balance {balance}")
    {

    }
}
