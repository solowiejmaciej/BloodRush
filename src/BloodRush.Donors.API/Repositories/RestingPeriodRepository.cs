using System.Data;
using BloodRush.API.Dtos;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces.Repositories;
using Dapper;


namespace BloodRush.API.Repositories;

public class RestingPeriodRepository : IRestingPeriodRepository
{
    private readonly IDbConnection _dbConnection;

    public RestingPeriodRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<RestingPeriodDto> GetRestingPeriodByDonorIdAsync(Guid donorId)
    {
        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<RestingPeriodDto>("SELECT * FROM DonorsRestingPeriodInfo WHERE DonorId = @DonorId", new {DonorId = donorId});
        _dbConnection.Close();
        var restingPeriod = result.FirstOrDefault();
        if (restingPeriod is null) throw new RestingPeriodNotFoundException();
        return restingPeriod;
    }

    public async Task UpdateRestingPeriodMonthsAsync(Guid donorId, int restingPeriodInMonths)
    {

        _dbConnection.Open();
        await _dbConnection.ExecuteAsync("UPDATE DonorsRestingPeriodInfo SET RestingPeriodInMonths = @RestingPeriodInMonths WHERE DonorId = @DonorId", new {DonorId = donorId, RestingPeriodInMonths = restingPeriodInMonths});
        _dbConnection.Close();
    }
    
}