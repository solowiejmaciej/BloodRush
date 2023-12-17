namespace BloodRush.DonationFacility.API.Exceptions;

public class BloodNeedNotFoundException : Exception
{
    public BloodNeedNotFoundException() : base("Blood need not found")
    {
        
    }
}