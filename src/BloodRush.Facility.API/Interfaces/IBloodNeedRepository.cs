using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IBloodNeedRepository
{
    Task CancelBloodNeedAsync(int bloodNeedId, CancellationToken cancellationToken = default);
    Task CreateBloodNeedAsync(BloodNeed bloodNeed, CancellationToken cancellationToken = default);
    Task UpdateNotifiedDonorsCountAsync(int notificationBloodNeedId, int donorsIdsCount, CancellationToken cancellationToken);
    Task<List<BloodNeed>> GetBloodNeedsAsync(int requestCollectionFacilityId, CancellationToken cancellationToken);
}