using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IBloodNeedService
{
    public Task CancelBloodNeedAsync(int bloodNeedId, CancellationToken cancellationToken = default); 
    public Task CreateBloodNeedAsync(BloodNeed bloodNeed, CancellationToken cancellationToken = default);
}