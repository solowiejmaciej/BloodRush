#region

using BloodRush.API.Entities.Enums;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface INotificationsRepository
{
    Task AddDefaultNotificationInfoAsync(Guid donorId);
    Task<DonorNotificationInfo> GetNotificationInfoByDonorIdAsync(Guid id);
    Task UpdatePushNotificationTokenAsync(Guid id, string token);
    Task ChangeDonorNotificationChanel(Guid id, ENotificationChannel channel);
}