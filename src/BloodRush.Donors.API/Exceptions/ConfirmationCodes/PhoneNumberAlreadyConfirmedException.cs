namespace BloodRush.API.Exceptions.ConfirmationCodes;

public class PhoneNumberAlreadyConfirmedException : Exception
{
    public PhoneNumberAlreadyConfirmedException() : base("Phone number already confirmed")
    {
        
    }
}