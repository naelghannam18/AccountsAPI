using System.Globalization;

namespace Domain.Exceptions;

public class AccountDoesNotExistException : Exception
{
    public AccountDoesNotExistException()
        : base("Account Error: Either Sender Or Receiver Do Not Exist.")
    {
        
    }

    public AccountDoesNotExistException(string id) 
        : base($"Account With Id {id} Does Not Exist.")
    {
        
    }
}
