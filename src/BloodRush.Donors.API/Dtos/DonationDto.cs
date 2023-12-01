using BloodRush.API.Entities.Enums;

namespace BloodRush.API.Dtos;

public class DonationDto
{
    public int Id { get; set; }
    public int DonationFacilityId { get; set; }
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}