namespace BloodRush.API.Exceptions.ConfirmationCodes;

public class InvalidCodeException : Exception
{
    public InvalidCodeException() : base("This code is invalid")
    {
        
    }
}