using System.Globalization;

namespace Domain.Exceptions;

public class AccountDoesNotExistError : Exception
{
    public AccountDoesNotExistError() : base()
    {
        
    }

    public AccountDoesNotExistError(string message) : base(message)
    {
        
    }

    public AccountDoesNotExistError(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
        
    }
}
