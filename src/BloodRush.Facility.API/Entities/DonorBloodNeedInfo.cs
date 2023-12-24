namespace BloodRush.DonationFacility.API.Entities;

public class DonorBloodNeedInfo
{
    public Guid Id { get; set; }
    public bool IsRestingPeriodActive { get; set; }
    public string HomeAddress { get; set; }
    public int MaxDonationRangeInKm { get; set; }
}