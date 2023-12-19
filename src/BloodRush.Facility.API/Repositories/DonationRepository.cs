using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Donation>> GetDonationsByDonorIdAsync(Guid donorId)
    {
       return await _facilityDbContext.Donations.Where(donation => donation.DonorId == donorId).ToListAsync();
    }

    public async Task<int> GetDonationsCountByDonorId(Guid donorId)
    {
        var donations = await GetDonationsByDonorIdAsync(donorId);
        return donations.Count;
    }

    public async Task<int> GetDonatedBloodQuantityByDonorId(Guid donorId)
    {
        var donations = await GetDonationsByDonorIdAsync(donorId);
        return donations.Sum(donation => donation.QuantityInMl);
    }
}