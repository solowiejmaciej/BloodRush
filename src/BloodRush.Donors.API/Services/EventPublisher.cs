#region

using BloodRush.API.Entities.Enums;
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

    public async Task PublishDonorCreatedEventAsync(Guid donorId, string donorPhoneNumber,
        CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorCreatedEvent(donorId, donorPhoneNumber), cancellationToken);
    }



    public async Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(new DonorDeletedEvent(requestDonorId), cancellationToken);
    }
    

    public async Task PublishBloodNeedCreatedEventAsync(int collectionFacilityId, bool isUrgent,
        CancellationToken cancellationToken = default)
    { 
        var bloodNeedCreatedEvent = new BloodNeedCreatedEvent { CollectionFacilityId = collectionFacilityId, IsUrgent = isUrgent}; 
        
        await _publishEndpoint.Publish(bloodNeedCreatedEvent, cancellationToken);
    }
}
