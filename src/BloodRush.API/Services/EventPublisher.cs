#region

using BloodRush.API.Interfaces;
using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using MassTransit;

#endregion

namespace BloodRush.API.Services;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(
        IPublishEndpoint publishEndpoint,
        ILogger<EventPublisher> logger
    )
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishDonorCreatedEventAsync(Guid donorId, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorCreatedEvent(donorId), cancellationToken);
    }

    public Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType,
        int collectionFacilityId,
        CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(new SendNotificationEvent
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationType = notificationType
        }, cancellationToken);
    }

    public async Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorDeletedEvent(requestDonorId), cancellationToken);
    }
}