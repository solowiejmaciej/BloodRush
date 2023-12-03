using System.Data;
using BloodRush.API.Dtos;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;
using Dapper;

namespace BloodRush.API.Repositories;

public class NotificationsRepository : INotificationsRepository
{
    private readonly IDbConnection _dbConnection;

    public NotificationsRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task UpdatePushTokenAsync(Guid donorId, string pushToken)
    {
        
        var sql = @"
            UPDATE DonorsNotificationInfo
            SET PushNotificationToken = @pushToken
            WHERE DonorId = @donorId";
        _dbConnection.Open();
        await _dbConnection.ExecuteAsync(sql, new {donorId, pushToken});
        _dbConnection.Close();
    }

    public async Task UpdateNotificationsChannelAsync(Guid donorId, ENotificationChannel channel)
    {
        var sql = @"
            UPDATE DonorsNotificationInfo
            SET NotificationChannel = @channel
            WHERE DonorId = @donorId";
        _dbConnection.Open();
        await _dbConnection.ExecuteAsync(sql, new {donorId, channel});
        _dbConnection.Close();

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