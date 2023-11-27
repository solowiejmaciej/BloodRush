using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IDonationRepository
{
    Task AddNewDonation(Donation donation);
    Task GetDonationsByDonorId(Guid donorId);
    Task GetDonationsCountByDonorId(Guid donorId);
    Task GetDonatedBloodQuantityByDonorId(Guid donorId);
}