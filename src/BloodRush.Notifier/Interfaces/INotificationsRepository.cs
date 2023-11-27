#region

using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface INotificationsRepository
{
    Task AddDefaultNotificationInfoAsync(Guid donorId);
    Task<DonorNotificationInfo> GetNotificationInfoByDonorIdAsync(Guid id);
    Task UpdatePushNotificationTokenAsync(Guid id, string token);
    Task ChangeDonorNotificationChannel(Guid id, ENotificationChannel channel);
    Task DeleteNotificationInfoAsync(Guid donorId);
    Task<NotificationContent> GetNotificationContentForFacilityAsync(int collectionFacilityId, ENotificationType notificationType);
}