using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Hangfire.Manager;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class AddNewBloodNeedCommandHandler : IRequestHandler<AddNewBloodNeedCommand>
{
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly IBloodNeedRepository _bloodNeedRepository;
    private readonly IJobManager _jobManager;


    public AddNewBloodNeedCommandHandler(
        IDonationFacilityRepository donationFacilityRepository,
        IBloodNeedRepository bloodNeedRepository,
        IJobManager jobManager
        )
    {
        _donationFacilityRepository = donationFacilityRepository;
        _bloodNeedRepository = bloodNeedRepository;
        _jobManager = jobManager;
    }
    public async Task Handle(AddNewBloodNeedCommand request, CancellationToken cancellationToken)
    {
        
        var donationFacility = await _donationFacilityRepository.GetDonationFacilityByIdAsync(request.CollectionFacilityId);
        if (donationFacility == null)
        {
            throw new DonationFacilityNotFoundException();
        }
        
        var bloodNeed = new Entities.BloodNeed
        {
            DonationFacilityId = request.CollectionFacilityId,
            IsUrgent = request.IsUrgent,
        };
        await _bloodNeedRepository.CreateBloodNeedAsync(bloodNeed, cancellationToken);
        _jobManager.EnqueueProcessBloodNeedCreatedJob(bloodNeed);
    }
}

public class AddNewBloodNeedCommand : IRequest
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrgent { get; set; }
}