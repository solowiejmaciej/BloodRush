using System.Data;
using BloodRush.API.Dtos;
using BloodRush.API.Interfaces.Repositories;
using Dapper;

namespace BloodRush.API.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly IDbConnection _dbConnection;

    public DonationRepository(
        IDbConnection dbConnection
    )
    {
        _dbConnection = dbConnection;
    }
    public async Task<List<DonationDto>> GetDonationsByUserIdAsync(Guid donorId)
    {
        var sql = @"SELECT * FROM Donations WHERE DonorId = @DonorId";
        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<DonationDto>(sql, new {DonorId = donorId});
        _dbConnection.Close();
        return result.ToList();
    }
}