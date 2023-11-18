#region

using BloodRush.Contracts.Events;
using BloodRush.Notifier.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Consumers;

public class DonorCreatedConsumer : IConsumer<DonorCreatedEvent>
{
    private readonly ILogger<DonorCreatedConsumer> _logger;
    private readonly INotificationsRepository _notificationsRepository;

    public DonorCreatedConsumer(
        ILogger<DonorCreatedConsumer> logger,
        INotificationsRepository notificationsRepository
    )
    {
        _logger = logger;
        _notificationsRepository = notificationsRepository;
    }


    public async Task Consume(ConsumeContext<DonorCreatedEvent> context)
    {
        var donorCreatedEvent = context.Message;
        _logger.LogInformation($"User created: {donorCreatedEvent.DonorId}");
        await _notificationsRepository.AddDefaultNotificationInfoAsync(donorCreatedEvent.DonorId);
    }
}