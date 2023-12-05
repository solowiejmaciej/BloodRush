namespace BloodRush.DonationFacility.API.Exceptions;

public class DonationFacilityNotFoundException : Exception
{
    public DonationFacilityNotFoundException() : base("Donation facility not found")
    {
        
    }
}