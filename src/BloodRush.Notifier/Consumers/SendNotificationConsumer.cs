#region

using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Consumers;

public class SendNotificationConsumer : IConsumer<SendNotificationEvent>
{
    private readonly INotificationBuilder _notificationBuilder;
    private readonly IDonorRepository _donorRepository;
    private readonly ISender _sender;

    public SendNotificationConsumer(
        INotificationBuilder notificationBuilder,
        IDonorRepository donorRepository,
        ILogger<SendNotificationConsumer> logger,
        ISender sender
    )
    {
        _notificationBuilder = notificationBuilder;
        _donorRepository = donorRepository;
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<SendNotificationEvent> context)
    {
        var notificationEvent = context.Message;
        
        //TODO Check if collection facility exists
        var donorExists = await _donorRepository.ExistsAsync(notificationEvent.DonorId);
        if (!donorExists)
        {
            throw new DonorNotFoundException();
        }
        
        if (notificationEvent.NotificationType == ENotificationType.Custom)
        {
            var customNotification = await _notificationBuilder.BuildCustomAsync(notificationEvent.DonorId, notificationEvent.CollectionFacilityId, notificationEvent.Title, notificationEvent.Message);
            await _sender.SendAsync(customNotification);
            return;
        }
        
        var notification = await _notificationBuilder.BuildAsync(notificationEvent.DonorId, notificationEvent.CollectionFacilityId, notificationEvent.NotificationType);

        await _sender.SendAsync(notification);
    }
}