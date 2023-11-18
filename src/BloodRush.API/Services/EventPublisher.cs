#region

using BloodRush.API.Interfaces;
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

    public async Task PublishDonorCreatedEventAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing DonorCreatedEvent");
        await _publishEndpoint.Publish(new DonorCreatedEvent(userId), cancellationToken);
    }
}