namespace Domain.Exceptions;

public class CustomerDoesNotExistError : Exception
{
    public CustomerDoesNotExistError(string message) : base(message)
    {
        
    }
}
