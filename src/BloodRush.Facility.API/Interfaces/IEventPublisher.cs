#region

#endregion

#region

using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Models.Notifications;

#endregion

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IEventPublisher
{
    Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType, int donationFacilityId,
        CancellationToken cancellationToken = default);
    
    Task PublishSendNotificationEventAsync(Guid donorId, int donationFacilityId, NotificationContent content ,CancellationToken cancellationToken = default);
}