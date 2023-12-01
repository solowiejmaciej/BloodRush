using BloodRush.API.Dtos;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces.Repositories;

namespace BloodRush.API.Repositories;

public class NotificationsRepository : INotificationsRepository
{
    public Task UpdatePushTokenAsync(Guid donorId, string pushToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateNotificationsChannelAsync(Guid donorId, ENotificationChannel channel)
    {
        throw new NotImplementedException();
    }

    public Task<List<NotificationDto>> GetNotificationsAsync(Guid donorId)
    {
        throw new NotImplementedException();
    }
}