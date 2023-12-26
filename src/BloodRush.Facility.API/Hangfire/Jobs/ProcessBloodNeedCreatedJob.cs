#region

using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Interfaces;
using Hangfire;
using Hangfire.Server;
using NotificationService.Hangfire;

#endregion

namespace BloodRush.DonationFacility.API.Hangfire.Jobs;

public sealed class ProcessBloodNeedCreatedJob
{
    private readonly ILogger<ProcessBloodNeedCreatedJob> _logger;
    private readonly IDonorRepository _donorRepository;
    private readonly IBloodNeedRepository _bloodNeedRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly ILocationService _locationService;
    


    public ProcessBloodNeedCreatedJob(
        IDonorRepository donorRepository,
        IBloodNeedRepository bloodNeedRepository,
        IEventPublisher eventPublisher,
        IDonationFacilityRepository donationFacilityRepository,
        ILocationService locationService, ILogger<ProcessBloodNeedCreatedJob> logger)
    {
        _donorRepository = donorRepository;
        _bloodNeedRepository = bloodNeedRepository;
        _eventPublisher = eventPublisher;
        _donationFacilityRepository = donationFacilityRepository;
        _locationService = locationService;
        _logger = logger;
    }
    

    [AutomaticRetry(Attempts = 0)]
    [JobDisplayName("ProcessBloodNeedCreatedJob")]
    [Queue(HangfireQueues.DEFAULT)]
    public async Task Execute(
        PerformContext context,
        CancellationToken cancellationToken,
        BloodNeed bloodNeed
        )
    {
        _logger.LogInformation($"Processing BloodNeedCreatedEvent with id {bloodNeed.Id} for donation facility {bloodNeed.DonationFacilityId}, jobId: {context.BackgroundJob.Id}");
        var donorsIds = bloodNeed.IsUrgent switch
        {
            true => await GetMatchingDonorsIds(),
            false => await GetMatchingDistanceDonorsIds(bloodNeed.DonationFacilityId)
        };
        
        await Task.WhenAll(donorsIds.Select(donorId => _eventPublisher.PublishSendNotificationEventAsync(donorId,
            ENotificationType.BloodNeed, bloodNeed.DonationFacilityId, cancellationToken)));
        await _bloodNeedRepository.UpdateNotifiedDonorsCountAsync(bloodNeed.Id, donorsIds.Count,
            cancellationToken);
        _logger.LogInformation($"Processed BloodNeedCreatedEvent with id {bloodNeed.Id} for donation facility {bloodNeed.DonationFacilityId}, jobId: {context.BackgroundJob.Id}");
        _logger.LogInformation($"Published events count: {donorsIds.Count}");
    }
    
    private async Task<List<Guid>> GetMatchingDistanceDonorsIds(int donationFacilityId)
    {
        var donorsBloodNeedInfo = await _donorRepository.GetBloodNeedInfoForNotRestingDonorsAsync();
        return donorsBloodNeedInfo.Where(donorInfo => IsInDistance(donorInfo, donationFacilityId).Result).Select(donorInfo => donorInfo.Id).ToList();
    }

    private async Task<bool> IsInDistance(DonorBloodNeedInfo donorInfo, int donationFacilityId)
    {
        var donationFacility = await _donationFacilityRepository.GetDonationFacilityByIdAsync(donationFacilityId);
        
        var distance = await _locationService.GetDistanceBetweenTwoAddresses(donorInfo.HomeAddress, donationFacility.Address);
        
        double maxDistance = donorInfo.MaxDonationRangeInKm;
        return distance <= maxDistance;
    }

    private async Task<List<Guid>> GetMatchingDonorsIds()
    {
        var result = await _donorRepository.GetBloodNeedInfoForNotRestingDonorsAsync();
        return result.Select(donorInfo => donorInfo.Id).ToList();
    }
    
}