using BloodRush.DonationFacility.API.Entities.Enums;

namespace BloodRush.DonationFacility.API.Models.Requests;

public class AddNewDonationRequest
{
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}