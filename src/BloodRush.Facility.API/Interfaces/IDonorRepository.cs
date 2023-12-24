using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IDonorRepository
{
    Task<List<Donor>> GetAllDonorsAsync();
    Task<Donor> GetDonorByIdAsync(Guid id);
    Task<Stream?> GetDonorPictureByDonorIdAsync(Guid donorId);
    Task<List<DonorBloodNeedInfo>> GetBloodNeedInfoForNotRestingDonorsAsync();
}