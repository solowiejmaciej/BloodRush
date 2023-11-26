namespace BloodRush.API.Exceptions;

public class DonorNotFoundException : Exception
{
    public DonorNotFoundException() : base("Donor not found")
    {
    }
}