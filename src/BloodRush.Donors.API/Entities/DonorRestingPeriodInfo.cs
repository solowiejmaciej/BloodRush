using System.ComponentModel.DataAnnotations.Schema;

namespace BloodRush.API.Entities;

public class DonorRestingPeriodInfo
{
    public Guid Id { get; set; }
    
    public Guid DonorId { get; set; }
    [ForeignKey(nameof(DonorId))] public Donor Donor { get; set; }

    public DateTime? LastDonationDate { get; set; } = null;
    public bool IsRestingPeriodActive { get; set; }
    public int RestingPeriodInMonths { get; set; }
}