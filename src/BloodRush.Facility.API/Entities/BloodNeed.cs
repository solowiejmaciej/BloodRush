namespace BloodRush.DonationFacility.API.Entities;

public class BloodNeed
{
    public int Id { get; set; }
    public int DonationFacilityId { get; set; }
    public bool IsUrgent { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime? CancellationDate { get; set; }
    public int NotifiedDonorsCount { get; set; }
}