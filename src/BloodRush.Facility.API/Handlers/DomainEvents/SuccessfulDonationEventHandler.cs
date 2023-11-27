using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.DomainEvents;

public class SuccessfulDonationEventHandler : INotificationHandler<SuccessfulDonationEvent>
{
    private readonly ILogger<SuccessfulDonationEventHandler> _logger;
    private readonly IDonorInfoRepository _donorInfoRepository;
    private readonly IEventPublisher _eventPublisher;

    public SuccessfulDonationEventHandler(
        ILogger<SuccessfulDonationEventHandler> logger,
        IDonorInfoRepository donorInfoRepository,
        IEventPublisher eventPublisher
        )
    {
        _logger = logger;
        _donorInfoRepository = donorInfoRepository;
        _eventPublisher = eventPublisher;
    }
    public async Task Handle(SuccessfulDonationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"SuccessfulDonationEvent received for donorId: {notification.DonorId}");
        await _donorInfoRepository.UpdateIsRestingPeriodActiveAsync(notification.DonorId, notification.DonationDate ,true);
        await _eventPublisher.PublishSendNotificationEventAsync(notification.DonorId, ENotificationType.ThankYou, notification.DonationFacilityId, cancellationToken);
    }
}