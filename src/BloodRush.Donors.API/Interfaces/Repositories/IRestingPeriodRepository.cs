using BloodRush.API.Dtos;

namespace BloodRush.API.Interfaces.Repositories;

public interface IRestingPeriodRepository
{
    Task<RestingPeriodDto> GetRestingPeriodByDonorIdAsync(Guid donorId);
    Task UpdateRestingPeriodMonthsAsync(Guid donorId, int restingPeriodInMonths);
}