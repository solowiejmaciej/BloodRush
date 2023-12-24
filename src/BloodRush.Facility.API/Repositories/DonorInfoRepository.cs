using BloodRush.DonationFacility.API.Constants;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Entities.Enums;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodRush.DonationFacility.API.Repositories;

public class DonorInfoRepository : IDonorInfoRepository
{
    private readonly BloodRushFacilityDbContext _context;
    private readonly IDonorRepository _donorRepository;

    public DonorInfoRepository(
        BloodRushFacilityDbContext context,
        IDonorRepository donorRepository
        )
    {
        _context = context;
        _donorRepository = donorRepository;
    }
    

    public async Task UpdateIsRestingPeriodActiveAsync(Guid donorId, DateTime notificationDonationDate,
        bool isRestingPeriodActive)
    {
        var restingPeriodInfo = await GetRestingPeriodInfoByDonorIdAsync(donorId);
        restingPeriodInfo.IsRestingPeriodActive = isRestingPeriodActive;
        restingPeriodInfo.LastDonationDate = notificationDonationDate;
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Returns <c>DonorRestingPeriodInfo</c> for given <c> donorId </c>
    /// If<c> DonorRestingPeriodInfo </c> does not exist, creates new one with default resting period
    /// only if Donor exists
    /// </summary>
    public async Task<DonorRestingPeriodInfo> GetRestingPeriodInfoByDonorIdAsync(Guid id)
    {
        var donor = await _donorRepository.GetDonorByIdAsync(id);
        if (donor is null)
        {
            throw new DonorNotFoundException();
        }
        var restingPeriodInfo = await _context.DonorsRestingPeriodInfo.FirstOrDefaultAsync(d => d.DonorId == id);
        if (restingPeriodInfo is null)
        {
            return await AddDefaultRestingPeriodInfoAsync(id);
        }
        return restingPeriodInfo;
    }

    public async Task UpdateRestingPeriodInfoAsync(Guid id, int restingPeriodInMonths)
    {
        var restingPeriodInfo = await GetRestingPeriodInfoByDonorIdAsync(id);
        restingPeriodInfo.RestingPeriodInMonths = restingPeriodInMonths;
        await _context.SaveChangesAsync();
    }
    
    private async Task<DonorRestingPeriodInfo> AddDefaultRestingPeriodInfoAsync(Guid donorId)
    {
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);

        if (donor.Sex == ESex.Female)
        {
            var restingPeriodFemale = new DonorRestingPeriodInfo
            {
                DonorId = donorId,
                RestingPeriodInMonths = DonorConstants.FemaleDefaultRestingPeriod
            };
            await _context.AddAsync(restingPeriodFemale);
            await _context.SaveChangesAsync();
            return restingPeriodFemale;
        }
        else
        {
            var restingPeriodMale = new DonorRestingPeriodInfo
            {
                DonorId = donorId,
                RestingPeriodInMonths = DonorConstants.MaleDefaultRestingPeriod
            };
            await _context.AddAsync(restingPeriodMale);
            await _context.SaveChangesAsync();
            return restingPeriodMale;
        }
    }
}