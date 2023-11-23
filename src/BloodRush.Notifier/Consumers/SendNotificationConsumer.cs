#region

using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Consumers;

public class SendNotificationConsumer : IConsumer<SendNotificationEvent>
{
    private readonly INotificationBuilder _notificationBuilder;
    private readonly ISender _sender;

    public SendNotificationConsumer(
        INotificationBuilder notificationBuilder,
        ILogger<SendNotificationConsumer> logger,
        ISender sender
    )
    {
        _notificationBuilder = notificationBuilder;
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<SendNotificationEvent> context)
    {
        var notificationEvent = context.Message;
        var notification = await _notificationBuilder.BuildAsync(notificationEvent);
        switch (notification.NotificationChannel)
        {
            case ENotificationChannel.Sms:
                await _sender.SendSmsAsync(notification);
                break;
            case ENotificationChannel.Push:
                await _sender.SendPushAsync(notification);
                break;
            default:
                throw new InvalidNotificationChannelException();
        }
    }
}