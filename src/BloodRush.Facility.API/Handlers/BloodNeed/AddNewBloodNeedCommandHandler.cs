using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class AddNewBloodNeedCommandHandler : IRequestHandler<AddNewBloodNeedCommand>
{
    private readonly IEventPublisher _eventPublisher;
    private readonly IDonationFacilityRepository _donationFacilityRepository;


    public AddNewBloodNeedCommandHandler(
        IEventPublisher eventPublisher,
        IDonationFacilityRepository donationFacilityRepository
        )
    {
        _eventPublisher = eventPublisher;
        _donationFacilityRepository = donationFacilityRepository;
    }
    public async Task Handle(AddNewBloodNeedCommand request, CancellationToken cancellationToken)
    {
        
        var donationFacility = await _donationFacilityRepository.GetDonationFacilityByIdAsync(request.CollectionFacilityId);
        
        if (donationFacility == null)
        {
            throw new DonationFacilityNotFoundException();
        }
        
        await _eventPublisher.PublishBloodNeedCreatedEventAsync(request.CollectionFacilityId, request.IsUrget ,cancellationToken);
    }
}

public class AddNewBloodNeedCommand : IRequest
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrget { get; set; }
}