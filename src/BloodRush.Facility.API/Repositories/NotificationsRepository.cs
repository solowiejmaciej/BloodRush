using System.Data;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using Dapper;

namespace BloodRush.DonationFacility.API.Repositories;

public class NotificationsRepository : INotificationsRepository
{
    private readonly IDbConnection _dbConnection;

    public NotificationsRepository(
        IDbConnection dbConnection
        )
    {
        _dbConnection = dbConnection;
    }
    public async Task<List<NotificationDto>> GetNotificationsAsync(Guid donorId)
    {
        var sql = "SELECT * FROM Notifications WHERE DonorId = @donorId";
        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<NotificationDto>(sql, new { donorId });
        _dbConnection.Close();
        return result.ToList();
    }
}