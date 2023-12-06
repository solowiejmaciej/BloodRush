namespace BloodRush.API.Exceptions.ConfirmationCodes;

public class EmailAlreadyConfirmedException : Exception
{
    public EmailAlreadyConfirmedException() : base("Email already confirmed")
    {
        
    }
}