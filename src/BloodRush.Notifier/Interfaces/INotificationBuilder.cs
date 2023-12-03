#region

using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface INotificationBuilder
{
    Task<Notification> BuildAsync(Guid donorId, int collectionFacilityId, ENotificationType notificationType);
    Task<Notification> BuildCustomAsync(Guid donorId, int collectionFacilityId, string title, string message);
}