using System.Data;
using BloodRush.Contracts.Enums;
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

    public async Task UpdateNotificationTemplateContent(ENotificationType requestNotificationType, int requestDonationFacilityId,
        string requestContent, string? requestTitle)
    {
        var sql = "UPDATE NotificationsContent SET Message = @requestContent, Title = @requestTitle WHERE NotificationType = @requestNotificationType AND CollectionFacilityId = @requestDonationFacilityId";
        _dbConnection.Open(); 
        await _dbConnection.ExecuteAsync(sql, new { requestContent, requestTitle, requestNotificationType , requestDonationFacilityId  }); 
        _dbConnection.Close();
        
    }

    public async Task<List<NotificationContentDto>> GetNotificationsContent(int requestDonationFacilityId)
    {
        var sql = "SELECT * FROM NotificationsContent WHERE CollectionFacilityId = @requestDonationFacilityId";
        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<NotificationContentDto>(sql, new { requestDonationFacilityId });
        _dbConnection.Close();
        return result.ToList();
    }
}