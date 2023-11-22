#region

#endregion

#region

using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.API.Interfaces;

public interface IEventPublisher
{
    Task PublishDonorCreatedEventAsync(Guid donorId, CancellationToken cancellationToken = default);

    Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType, int collectionFacilityId,
        CancellationToken cancellationToken = default);

    Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default);
}