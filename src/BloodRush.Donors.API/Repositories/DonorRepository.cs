#region

using System.Linq.Expressions;
using BloodRush.API.Entities;
using BloodRush.API.Entities.DbContext;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
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

    public async Task<Donor?> GetDonorByPhoneNumberAsync(string username)
    {
        var donor = await _context.Donors.SingleOrDefaultAsync(d => d.PhoneNumber == username);
        return donor;
    }

    public async Task<bool> DeleteDonorAsync(Guid donorId)
    {
        var donor = await GetDonorByIdAsync(donorId);
        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Donor?> GetDonorByEmailAsync(string email)
    {
        var donor = await _context.Donors.SingleOrDefaultAsync(d => d.Email == email);
        return donor;
    }
}