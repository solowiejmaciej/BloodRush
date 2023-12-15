namespace BloodRush.API.Dtos;

public class RestingPeriodDto
{
    public Guid DonorId { get; set; }
    public DateTime? LastDonationDate { get; set; } = null;
    public bool IsRestingPeriodActive { get; set; }
    public int RestingPeriodInMonths { get; set; }
    public int RemainingDays { get; set; }
}