using System.ComponentModel.DataAnnotations.Schema;
using BloodRush.API.Entities.Enums;

namespace BloodRush.API.Entities;

public class Donation
{
    public int Id { get; set; }
    public Guid DonorId { get; set; }
    [ForeignKey(nameof(DonorId))] public Donor Donor { get; set; }
    
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}