using System.ComponentModel.DataAnnotations.Schema;

namespace BloodRush.DonationFacility.API.Entities;

public class DonorRestingPeriodInfo
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public DateTime? LastDonationDate { get; set; } = null;
    public bool IsRestingPeriodActive { get; set; }
    public int RestingPeriodInMonths { get; set; }
}