using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Hangfire.Manager;
using BloodRush.DonationFacility.API.Interfaces;
using MassTransit.Initializers;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.DomainEvents;

public class BloodNeedCreatedEventHandler : INotificationHandler<BloodNeedCreatedEvent>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IBloodNeedRepository _bloodNeedRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly ILocationService _locationService;
    private readonly IJobManager _jobManager;

    public BloodNeedCreatedEventHandler(
        IDonorRepository donorRepository,
        IBloodNeedRepository bloodNeedRepository,
        IEventPublisher eventPublisher,
        IDonationFacilityRepository donationFacilityRepository,
        ILocationService locationService,
        IJobManager jobManager
    )
    {
        _donorRepository = donorRepository;
        _bloodNeedRepository = bloodNeedRepository;
        _eventPublisher = eventPublisher;
        _donationFacilityRepository = donationFacilityRepository;
        _locationService = locationService;
        _jobManager = jobManager;
    }

    public async Task Handle(BloodNeedCreatedEvent notification, CancellationToken cancellationToken)
    {
        _jobManager.EnqueueProcessBloodNeedCreatedJob(notification);
    }
}

