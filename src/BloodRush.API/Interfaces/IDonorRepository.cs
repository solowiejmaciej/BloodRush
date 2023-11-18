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
    Task<List<Donor?>?> GetDonorsByConditionAsync(Expression<Func<Donor?, bool>> expression);

    Task<DonorRestingPeriodInfo> GetRestingPeriodInfoByDonorIdAsync(Guid id);
    Task UpdateRestingPeriodInfoAsync(Guid id, int restingPeriodInMonths);
}