#region

using BloodRush.Contracts.Events;
using BloodRush.Notifier.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Consumers;

public class DonorDeletedConsumer : IConsumer<DonorDeletedEvent>
{
    private readonly ILogger<DonorCreatedConsumer> _logger;
    private readonly INotificationsRepository _notificationsRepository;

    public DonorDeletedConsumer(
        ILogger<DonorCreatedConsumer> logger,
        INotificationsRepository notificationsRepository
    )
    {
        _logger = logger;
        _notificationsRepository = notificationsRepository;
    }


    public async Task Consume(ConsumeContext<DonorDeletedEvent> context)
    {
        var donorCreatedEvent = context.Message;
        _logger.LogInformation($"User deleted: {donorCreatedEvent.DonorId}");
        await _notificationsRepository.DeleteNotificationInfoAsync(donorCreatedEvent.DonorId);
    }
}