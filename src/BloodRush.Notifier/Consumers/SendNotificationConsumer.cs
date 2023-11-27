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
        //TODO Check if collection facility exists
        var notification = await _notificationBuilder.BuildAsync(notificationEvent.DonorId, notificationEvent.PhoneNumber ,notificationEvent.CollectionFacilityId, notificationEvent.NotificationType);

        await _sender.SendAsync(notification);
    }
}