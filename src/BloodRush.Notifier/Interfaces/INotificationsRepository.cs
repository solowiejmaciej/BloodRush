#region

using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface INotificationsRepository
{
    Task AddDefaultNotificationInfoAsync(Guid donorId);
    Task<DonorNotificationInfo> GetNotificationInfoByDonorIdAsync(Guid id);
    Task DeleteNotificationInfoAsync(Guid donorId);

    Task<NotificationContent> GetNotificationContentForFacilityAsync(int collectionFacilityId,
        ENotificationType notificationType);
    Task AddNotificationAsync (Notification notification);
    Task<List<Notification>> GetNotificationsByDonorIdAsync(Guid donorId);
}