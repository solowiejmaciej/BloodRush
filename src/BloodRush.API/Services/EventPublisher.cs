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
    private readonly IDonorRepository _donorRepository;

    public EventPublisher(
        IPublishEndpoint publishEndpoint,
        ILogger<EventPublisher> logger, IDonorRepository donorRepository)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _donorRepository = donorRepository;
    }

    public async Task PublishDonorCreatedEventAsync(Guid donorId, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorCreatedEvent(donorId), cancellationToken);
    }

    public async Task PublishSendNotificationEventAsync(Guid donorId, ENotificationType notificationType,
        int collectionFacilityId,
        CancellationToken cancellationToken = default)
    {
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);
        await _publishEndpoint.Publish(new SendNotificationEvent
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationType = notificationType,
            PhoneNumber = donor.PhoneNumber
        }, cancellationToken);
    }

    public async Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorDeletedEvent(requestDonorId), cancellationToken);
    }
}