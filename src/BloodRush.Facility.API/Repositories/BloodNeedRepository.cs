using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodRush.DonationFacility.API.Repositories;

public class BloodNeedRepository : IBloodNeedRepository
{
    private readonly BloodRushFacilityDbContext _dbContext;

    public BloodNeedRepository(
        BloodRushFacilityDbContext dbContext
        )
    {
        _dbContext = dbContext;
    }
    public Task CancelBloodNeedAsync(int bloodNeedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateBloodNeedAsync(BloodNeed bloodNeed, CancellationToken cancellationToken = default)
    {
       await _dbContext.BloodNeeds.AddAsync(bloodNeed, cancellationToken);
       await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateNotifiedDonorsCountAsync(int bloodNeedId, int donorsIdsCount,
        CancellationToken cancellationToken)
    {
        var bloodNeed = await _dbContext.BloodNeeds.FirstOrDefaultAsync(bloodNeed => bloodNeed.Id == bloodNeedId, cancellationToken: cancellationToken);
        if (bloodNeed is null)
        {
            throw new BloodNeedNotFoundException();
        }
        bloodNeed.NotifiedDonorsCount = donorsIdsCount;
        _dbContext.BloodNeeds.Update(bloodNeed);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BloodNeed>> GetBloodNeedsAsync(int requestCollectionFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.BloodNeeds.Where(bloodNeed => bloodNeed.DonationFacilityId == requestCollectionFacilityId).ToListAsync(cancellationToken);
    }
}