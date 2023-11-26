using BloodRush.API.Entities;
using BloodRush.API.Entities.DbContext;
using BloodRush.API.Interfaces;

namespace BloodRush.API.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly BloodRushDbContext _dbContext;

    public DonationRepository(
        BloodRushDbContext dbContext
        )
    {
        _dbContext = dbContext;
    }
    public async Task AddNewDonation(Donation donation)
    {
        await _dbContext.Donations.AddAsync(donation);
        await _dbContext.SaveChangesAsync();
    }

    public Task GetDonationsByDonorId(Guid donorId)
    {
        throw new NotImplementedException();
    }

    public Task GetDonationsCountByDonorId(Guid donorId)
    {
        throw new NotImplementedException();
    }

    public Task GetDonatedBloodQuantityByDonorId(Guid donorId)
    {
        throw new NotImplementedException();
    }
}