namespace Domain.Exceptions;

public class CustomerDoesNotExistException : Exception
{
    
    public CustomerDoesNotExistException(string id) 
        : base($"Customer with Id {id} Does not Exist")
    {
        
    }
}
