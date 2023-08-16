namespace Domain.Exceptions;

public class ModelValidationException : Exception
{
    public ModelValidationException(List<string> errors)
        : base($"Model Validation Failed: Errors:\t {string.Join("\t", errors)}")
    {
        
    }
}
