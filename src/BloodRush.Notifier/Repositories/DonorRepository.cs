using System.Data;
using BloodRush.Notifier.Interfaces;
using Dapper;

namespace BloodRush.Notifier.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly IDbConnection _dbConnection;

    public DonorRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<bool> ExistsAsync(Guid donorId)
    {
        var sql = "SELECT Id FROM Donors WHERE Id = @Id";
        _dbConnection.Open();
        var query = await _dbConnection.QueryFirstOrDefaultAsync<Guid>(sql, new { Id = donorId });
        _dbConnection.Close();
        return query != Guid.Empty;
    }

    public async Task<string?> GetPhoneNumberAsync(Guid donorId)
    {
        var sql = "SELECT PhoneNumber FROM Donors WHERE Id = @Id";
        _dbConnection.Open();
        var res = await _dbConnection.QueryFirstOrDefaultAsync<string>(sql, new { Id = donorId });
        _dbConnection.Close();
        return res;
    }

    public async Task<string?> GetEmailAsync(Guid donorId)
    {
        var sql = "SELECT Email FROM Donors WHERE Id = @Id";
        _dbConnection.Open();
        var res = await _dbConnection.QueryFirstOrDefaultAsync<string>(sql, new { Id = donorId });
        return res;
    }
}