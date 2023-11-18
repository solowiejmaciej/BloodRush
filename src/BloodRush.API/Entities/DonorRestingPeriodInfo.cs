namespace BloodRush.API.Entities;

public class DonorRestingPeriodInfo
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public int RestingPeriodInMonths { get; set; }
}