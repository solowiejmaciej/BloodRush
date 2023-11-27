#region

using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Consumers;

public class DonorCreatedConsumer : IConsumer<DonorCreatedEvent>
{
    private readonly ILogger<DonorCreatedConsumer> _logger;
    private readonly INotificationsRepository _notificationsRepository;
    private readonly INotificationBuilder _notificationBuilder;
    private readonly ISender _sender;

    public DonorCreatedConsumer(
        ILogger<DonorCreatedConsumer> logger,
        INotificationsRepository notificationsRepository,
        INotificationBuilder notificationBuilder,
        ISender sender
    )
    {
        _logger = logger;
        _notificationsRepository = notificationsRepository;
        _notificationBuilder = notificationBuilder;
        _sender = sender;
    }


    public async Task Consume(ConsumeContext<DonorCreatedEvent> context)
    {
        var donorCreatedEvent = context.Message;
        _logger.LogInformation($"User created: {donorCreatedEvent.DonorId}");
        await _notificationsRepository.AddDefaultNotificationInfoAsync(donorCreatedEvent.DonorId);
        var notification = await _notificationBuilder.BuildAsync(donorCreatedEvent.DonorId, donorCreatedEvent.PhoneNumber, -1, ENotificationType.Welcome);
        await _sender.SendAsync(notification);
    }
}