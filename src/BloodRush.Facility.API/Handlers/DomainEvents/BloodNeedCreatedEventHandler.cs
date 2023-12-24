using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.DomainEvents;

public class BloodNeedCreatedEventHandler : INotificationHandler<BloodNeedCreatedEvent>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IDonorInfoRepository _donorInfoRepository;
    private readonly IBloodNeedRepository _bloodNeedRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly ILocationService _locationService;

    public BloodNeedCreatedEventHandler(
        IDonorRepository donorRepository,
        IDonorInfoRepository donorInfoRepository,
        IBloodNeedRepository bloodNeedRepository,
        IEventPublisher eventPublisher,
        IDonationFacilityRepository donationFacilityRepository,
        ILocationService locationService
    )
    {
        _donorRepository = donorRepository;
        _donorInfoRepository = donorInfoRepository;
        _bloodNeedRepository = bloodNeedRepository;
        _eventPublisher = eventPublisher;
        _donationFacilityRepository = donationFacilityRepository;
        _locationService = locationService;
    }

    public async Task Handle(BloodNeedCreatedEvent notification, CancellationToken cancellationToken)
    {
        //TODO this needs to be refactored to use background jobs, maybe Quartz?
        var donorsIds = notification.IsUrgent switch
        {
            true => await GetNotRestingDonorsAsync(),
            false => await GetMatchingDistanceDonorsIds(notification.DonationFacilityId)
        };

        await Task.WhenAll(donorsIds.Select(donorId => _eventPublisher.PublishSendNotificationEventAsync(donorId,
            ENotificationType.BloodNeed, notification.DonationFacilityId, cancellationToken)));
        await _bloodNeedRepository.UpdateNotifiedDonorsCountAsync(notification.BloodNeedId, donorsIds.Count,
            cancellationToken);
    }

    private async Task<List<Guid>> GetMatchingDistanceDonorsIds(int donationFacilityId)
    {
        var potentialDonorsIds = new List<Guid>();
        var notRestingDonorsIds = await GetNotRestingDonorsAsync();
        foreach (var donorsId in notRestingDonorsIds)
        {
            var donor = await _donorRepository.GetDonorByIdAsync(donorsId);
            if (donor is null)
            {
                throw new DonorNotFoundException();
            }

            if (await IsInDistance(donor, donationFacilityId))
            {
                potentialDonorsIds.Add(donorsId);
            }
        }

        return potentialDonorsIds;
    }

    private async Task<bool> IsInDistance(Donor donor, int donationFacilityId)
    {
        var donationFacility = _donationFacilityRepository.GetDonationFacilityByIdAsync(donationFacilityId).Result;
        if (donationFacility is null)
        {
            throw new DonationFacilityNotFoundException();
        }
        
        var distance = await _locationService.GetDistanceBetweenTwoAddresses(donor.HomeAddress, donationFacility.Address);
        
        double maxDistance = donor.MaxDonationRangeInKm;
        return distance <= maxDistance;
    }

    private async Task<List<Guid>> GetNotRestingDonorsAsync()
    {
        var donors = await _donorRepository.GetAllDonorsAsync();
        var notRestingDonorsIds = new List<Guid>();
        foreach (var donor in donors)
        {
            var donorInfo = await _donorInfoRepository.GetRestingPeriodInfoByDonorIdAsync(donor.Id);
            
            if (!donorInfo.IsRestingPeriodActive)
            {
                notRestingDonorsIds.Add(donor.Id);
            }
        }
        
        return notRestingDonorsIds;
    }
}

