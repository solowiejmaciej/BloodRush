using BloodRush.API.Entities;

namespace BloodRush.API.Interfaces;

public interface IDonationRepository
{
    Task AddNewDonation(Donation donation);
    Task GetDonationsByDonorId(Guid donorId);
    Task GetDonationsCountByDonorId(Guid donorId);
    Task GetDonatedBloodQuantityByDonorId(Guid donorId);
}