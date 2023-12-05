using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodRush.DonationFacility.API.Repositories;

public class DonationFacilityRepository : IDonationFacilityRepository
{
    private readonly BloodRushFacilityDbContext _bloodRushFacilityDbContext;

    public DonationFacilityRepository(
        BloodRushFacilityDbContext bloodRushFacilityDbContext
        )
    {
        _bloodRushFacilityDbContext = bloodRushFacilityDbContext;
    }
    
    public async Task AddDonationFacilityAsync(Entities.DonationFacility donationFacilityDto)
    {
        await _bloodRushFacilityDbContext.DonationFacilities.AddAsync(donationFacilityDto);
        await _bloodRushFacilityDbContext.SaveChangesAsync();
    }

    public async Task<List<Entities.DonationFacility>> GetDonationFacilitiesAsync()
    {
        return await _bloodRushFacilityDbContext.DonationFacilities.ToListAsync();
    }

    public async Task<Entities.DonationFacility> GetDonationFacilityByIdAsync(int id)
    {
        var result = await _bloodRushFacilityDbContext.DonationFacilities.FirstOrDefaultAsync(x => x.Id == id);
        
        if (result == null) throw new DonationFacilityNotFoundException();
        
        return result;
    }
}