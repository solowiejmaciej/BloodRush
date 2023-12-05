using BloodRush.DonationFacility.API.Dtos;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IDonationFacilityRepository
{
    public Task AddDonationFacilityAsync(Entities.DonationFacility donationFacilityDto);
    public Task<List<Entities.DonationFacility>> GetDonationFacilitiesAsync();
    public Task<Entities.DonationFacility> GetDonationFacilityByIdAsync(int id);
}