using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IDonationRepository
{
    Task AddNewDonation(Donation donation);
    Task<List<Donation>> GetDonationsByDonorIdAsync(Guid donorId);
    Task<int> GetDonationsCountByDonorId(Guid donorId);
    Task<int> GetDonatedBloodQuantityByDonorId(Guid donorId);
}