using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class AddNewBloodNeedCommandHandler : IRequestHandler<AddNewBloodNeedCommand>
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly IBloodNeedService _bloodNeedService;


    public AddNewBloodNeedCommandHandler(
        IEventPublisher eventPublisher,
        IDonationFacilityRepository donationFacilityRepository,
        IBloodNeedService bloodNeedService
        )
    {
        _eventPublisher = eventPublisher;
        _donationFacilityRepository = donationFacilityRepository;
        _bloodNeedService = bloodNeedService;
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
            IsUrgent = request.IsUrget,
        }; 
        
        await _bloodNeedService.CreateBloodNeedAsync(bloodNeed,cancellationToken);
    }
}

public class AddNewBloodNeedCommand : IRequest
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrget { get; set; }
}