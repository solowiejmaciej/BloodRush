namespace BloodRush.API.Exceptions;

public class RestingPeriodNotFoundException : Exception
{
    public RestingPeriodNotFoundException() : base("Resting period not found")
    {
    }
}