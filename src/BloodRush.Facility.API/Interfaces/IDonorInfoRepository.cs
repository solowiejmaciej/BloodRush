using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IDonorInfoRepository
{
    Task UpdateIsRestingPeriodActiveAsync(Guid donorId, DateTime notificationDonationDate, bool isRestingPeriodActive);
    Task<DonorRestingPeriodInfo> GetRestingPeriodInfoByDonorIdAsync(Guid id);
    Task<List<Guid>> GetNotRestingDonorsIdsAsync();
}
