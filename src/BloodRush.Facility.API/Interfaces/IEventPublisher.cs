#region

#endregion

#region

using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IEventPublisher
{
    Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType, int donationFacilityId,
        CancellationToken cancellationToken = default);

    Task PublishBloodNeedCreatedEventAsync(int collectionFacilityId, bool isUrgent ,CancellationToken cancellationToken = default);
}