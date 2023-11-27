namespace BloodRush.DonationFacility.API.Exceptions;

public class RestingPeriodNotFoundException : Exception
{
    public RestingPeriodNotFoundException() : base("Resting period not found")
    {
    }
}