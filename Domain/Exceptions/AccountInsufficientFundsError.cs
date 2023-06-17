using System.Globalization;

namespace Domain.Exceptions;

public class AccountInsufficientFundsError : Exception
{
    public AccountInsufficientFundsError() : base()
    {
        
    }
    public AccountInsufficientFundsError(string message) : base(message)
    {
        
    }
    public AccountInsufficientFundsError(string message, params object[] args)
        :base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
        
    }
}
