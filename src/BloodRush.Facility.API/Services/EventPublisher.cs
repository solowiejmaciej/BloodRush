#region

using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.DonationFacility.API.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.DonationFacility.API.Services;

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

    public async Task PublishBloodNeedCreatedEventAsync(int collectionFacilityId, bool isUrgent,
        CancellationToken cancellationToken = default)
    { 
        var bloodNeedCreatedEvent = new BloodNeedCreatedEvent { CollectionFacilityId = collectionFacilityId, IsUrgent = isUrgent}; 
        
        await _publishEndpoint.Publish(bloodNeedCreatedEvent, cancellationToken);
    }
}
