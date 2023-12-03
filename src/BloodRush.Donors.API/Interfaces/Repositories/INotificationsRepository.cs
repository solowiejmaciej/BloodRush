using BloodRush.API.Dtos;
using BloodRush.Contracts.Enums;

namespace BloodRush.API.Interfaces.Repositories;

public interface INotificationsRepository
{
    Task UpdatePushTokenAsync(Guid donorId, string pushToken);
    Task UpdateNotificationsChannelAsync(Guid donorId, ENotificationChannel channel);
    Task<List<NotificationDto>> GetNotificationsAsync(Guid donorId);
}