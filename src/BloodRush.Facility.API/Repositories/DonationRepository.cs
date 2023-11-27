using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Interfaces;

namespace BloodRush.DonationFacility.API.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly BloodRushFacilityDbContext _facilityDbContext;

    public DonationRepository(
        BloodRushFacilityDbContext facilityDbContext
        )
    {
        _facilityDbContext = facilityDbContext;
    }
    public async Task AddNewDonation(Donation donation)
    {
        await _facilityDbContext.Donations.AddAsync(donation);
        await _facilityDbContext.SaveChangesAsync();
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