namespace BloodRush.API.Exceptions;

public class DonorIsRestingException : Exception
{
    public DonorIsRestingException() : base("Donor is in resting period")
    {
        
    }
}