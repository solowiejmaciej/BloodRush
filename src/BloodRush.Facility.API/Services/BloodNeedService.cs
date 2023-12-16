using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Interfaces;

namespace BloodRush.DonationFacility.API.Services;

public class BloodNeedService : IBloodNeedService
{
    private readonly IBloodNeedRepository _bloodNeedRepository;

    public BloodNeedService(IBloodNeedRepository bloodNeedRepository)
    {
        _bloodNeedRepository = bloodNeedRepository;
    }
    public async Task CancelBloodNeedAsync(int bloodNeedId, CancellationToken cancellationToken = default)
    { 
        await _bloodNeedRepository.CancelBloodNeedAsync(bloodNeedId, cancellationToken);
        //Notify donors
    }

    
    //TODO: Implement logic to add new blood need and aggregate donors, then publish events to sent notification to donors
    //This was moved here from BloodRush.Task to keep the logic in one place
    public async Task CreateBloodNeedAsync(BloodNeed bloodNeed, CancellationToken cancellationToken = default)
    {
        var donorsIds = await GetPotentialDonorsIds();
        await _bloodNeedRepository.CreateBloodNeedAsync(bloodNeed, cancellationToken);
        //Notify donors
        throw new NotImplementedException();
    }

    private Task<List<Guid>> GetPotentialDonorsIds()
    {
        throw new NotImplementedException();
    }
}