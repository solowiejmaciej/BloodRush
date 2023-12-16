using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Interfaces;

namespace BloodRush.DonationFacility.API.Repositories;

public class BloodNeedRepository : IBloodNeedRepository
{
    public Task CancelBloodNeedAsync(int bloodNeedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateBloodNeedAsync(BloodNeed bloodNeed, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}