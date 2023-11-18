#region

using System.Linq.Expressions;
using BloodRush.API.Constants;
using BloodRush.API.Entities;
using BloodRush.API.Entities.DbContext;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly BloodRushDbContext _context;

    public DonorRepository(
        BloodRushDbContext context
    )
    {
        _context = context;
    }

    public async Task<Guid> AddDonorAsync(Donor donor)
    {
        var addedDonor = await _context.AddAsync(donor);
        await AddDefaultRestingPeriodInfoAsync(addedDonor.Entity.Id);
        await _context.SaveChangesAsync();
        return addedDonor.Entity.Id;
    }

    public async Task<Donor> GetDonorByIdAsync(Guid id)
    {
        var result = await _context.Donors.SingleOrDefaultAsync(d => d.Id == id);
        if (result == null) throw new DonorNotFoundException();
        return result;
    }

    public async Task<List<Donor>> GetAllDonorsAsync()
    {
        return await _context.Donors.ToListAsync();
    }

    public async Task<List<Donor?>?> GetDonorsByConditionAsync(Expression<Func<Donor?, bool>> expression)
    {
        return await _context.Donors.Where(expression).ToListAsync();
    }

    private async Task AddDefaultRestingPeriodInfoAsync(Guid donorId)
    {
        var donor = await GetDonorByIdAsync(donorId);

        if (donor.Sex == ESex.Female)
        {
            await _context.AddAsync(new DonorRestingPeriodInfo
            {
                DonorId = donorId,
                RestingPeriodInMonths = DonorConstants.FemaleDefaultRestingPeriod
            });
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.AddAsync(new DonorRestingPeriodInfo
            {
                DonorId = donorId,
                RestingPeriodInMonths = DonorConstants.MaleDefaultRestingPeriod
            });
            await _context.SaveChangesAsync();
        }
    }

    public async Task<DonorRestingPeriodInfo> GetRestingPeriodInfoByDonorIdAsync(Guid id)
    {
        var restingPeriodInfo = await _context.DonorsRestingPeriodInfo.FirstOrDefaultAsync(d => d.DonorId == id);
        if (restingPeriodInfo is null) throw new RestingPeriodNotFoundException();
        return restingPeriodInfo;
    }

    public async Task UpdateRestingPeriodInfoAsync(Guid id, int restingPeriodInMonths)
    {
        var restingPeriodInfo = await GetRestingPeriodInfoByDonorIdAsync(id);
        restingPeriodInfo.RestingPeriodInMonths = restingPeriodInMonths;
        await _context.SaveChangesAsync();
    }
}