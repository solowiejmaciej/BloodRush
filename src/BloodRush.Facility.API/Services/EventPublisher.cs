#region

using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Models.Notifications;
using MassTransit;

#endregion

namespace BloodRush.DonationFacility.API.Services;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(
        IPublishEndpoint publishEndpoint,
        ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }
    

    public async Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType,
        int collectionFacilityId,
        CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new SendNotificationEvent
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationType = notificationType,
        }, cancellationToken);
    }

    public async Task PublishSendNotificationEventAsync(Guid donorId, int donationFacilityId, NotificationContent content,
        CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new SendNotificationEvent
        {
            DonorId = donorId,
            CollectionFacilityId = donationFacilityId,
            NotificationType = ENotificationType.Custom,
            Title = content.Title,
            Message = content.Message
        }, cancellationToken);
    }
}
