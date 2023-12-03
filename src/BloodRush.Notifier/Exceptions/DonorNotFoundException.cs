namespace BloodRush.Notifier.Exceptions;

public class DonorNotFoundException : Exception
{
    public DonorNotFoundException() : base("Donor not found")
    {
        
    }
}