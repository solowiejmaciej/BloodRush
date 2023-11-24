using BloodRush.API.DomainEvents;
using BloodRush.API.Interfaces;
using BloodRush.Contracts.Enums;
using MediatR;

namespace BloodRush.API.Handlers.DomainEvents;

public class SuccessfulDonationEventHandler : INotificationHandler<SuccessfulDonationEvent>
{
    private readonly ILogger<SuccessfulDonationEventHandler> _logger;
    private readonly IDonorRepository _donorRepository;
    private readonly IEventPublisher _eventPublisher;

    public SuccessfulDonationEventHandler(
        ILogger<SuccessfulDonationEventHandler> logger,
        IDonorRepository donorRepository,
        IEventPublisher eventPublisher
        )
    {
        _logger = logger;
        _donorRepository = donorRepository;
        _eventPublisher = eventPublisher;
    }
    public async Task Handle(SuccessfulDonationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SuccessfulDonationEvent received for donorId: {notification.DonorId}");
        await _donorRepository.UpdateIsRestingPeriodActiveAsync(notification.DonorId, notification.DonationDate ,true);
        await _eventPublisher.PublishSendNotificationEventAsync(notification.DonorId, ENotificationType.ThankYou, 1, cancellationToken);
    }
}