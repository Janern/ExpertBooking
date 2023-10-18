namespace UseCases.Exceptions;

public class InvalidEditCartRequestException : Exception
{
    public InvalidEditCartRequestException(string errorMessage) : base(errorMessage)
    {
    }    
}
