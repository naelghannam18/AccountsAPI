namespace Domain.Exceptionsl;

public class TransactionNotFoundException : Exception
{
    public TransactionNotFoundException(string id)
        : base($"Transaction with Id {id} Not Found.")
    {
        
    }
}
