using BloodRush.Contracts.Events;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using MassTransit;

namespace BloodRush.Notifier.Consumers;

public class SendConfirmationCodeConsumer : IConsumer<SendConfirmationCodeEvent>
{
    private readonly INotificationBuilder _notificationBuilder;
    private readonly IDonorRepository _donorRepository;
    private readonly ISender _sender;

    public SendConfirmationCodeConsumer(
        INotificationBuilder notificationBuilder,
        IDonorRepository donorRepository,
        ILogger<SendConfirmationCodeConsumer> logger,
        ISender sender
    )
    {
        _notificationBuilder = notificationBuilder;
        _donorRepository = donorRepository;
        _sender = sender;
    }
    public async Task Consume(ConsumeContext<SendConfirmationCodeEvent> context)
    {
        var notificationEvent = context.Message;
        
        //TODO Check if collection facility exists
        var donorExists = await _donorRepository.ExistsAsync(notificationEvent.DonorId);
        if (!donorExists)
        {
            throw new DonorNotFoundException();
        }
        
        var notification = await _notificationBuilder.BuildConfirmationCodeNotification(notificationEvent.DonorId, notificationEvent.Code); 
        await  _sender.SendAsync(notification);
        
    }
}