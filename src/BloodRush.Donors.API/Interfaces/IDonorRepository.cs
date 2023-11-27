#region

using System.Linq.Expressions;
using BloodRush.API.Entities;

#endregion

namespace BloodRush.API.Interfaces;

public interface IDonorRepository
{
    Task<Guid> AddDonorAsync(Donor donor);
    Task<Donor> GetDonorByIdAsync(Guid id);
    Task<List<Donor>> GetAllDonorsAsync();
    Task<Donor?> GetDonorByPhoneNumberAsync(string phoneNumber);
    Task<bool> DeleteDonorAsync(Guid requestId);
}