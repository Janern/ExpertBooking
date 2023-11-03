namespace UseCases.Exceptions;

public class InvalidBookingException : Exception
{
    public InvalidBookingException(string errorMessage) : base(errorMessage)
    {
    }
}