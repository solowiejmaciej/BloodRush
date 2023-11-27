#region

using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface INotificationBuilder
{
    Task<Notification> BuildAsync(Guid donorId, string phoneNumber, int collectionFacilityId,
        ENotificationType notificationType);
}