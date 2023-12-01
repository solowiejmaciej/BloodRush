using BloodRush.DonationFacility.API.Entities.Enums;

namespace BloodRush.DonationFacility.API.Dtos;

public class DonationDto
{
    public int Id { get; set; }
    public Guid DonorId { get; set; }
    public int DonationFacilityId { get; set; }
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}