using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class AddNewBloodNeedCommandHandler : IRequestHandler<AddNewBloodNeedCommand>
{
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly IMediator _mediator;
    private readonly IBloodNeedRepository _bloodNeedRepository;


    public AddNewBloodNeedCommandHandler(
        IDonationFacilityRepository donationFacilityRepository,
        IMediator mediator,
        IBloodNeedRepository bloodNeedRepository
        )
    {
        _donationFacilityRepository = donationFacilityRepository;
        _mediator = mediator;
        _bloodNeedRepository = bloodNeedRepository;
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
        //TODO: Add event for urgent blood need
        await _mediator.Publish(
            new BloodNeedCreatedEvent()
            {
                BloodNeedId = bloodNeed.Id,
                DonationFacilityId = bloodNeed.DonationFacilityId,
                IsUrgent = bloodNeed.IsUrgent
            },
            cancellationToken);
    }
}

public class AddNewBloodNeedCommand : IRequest
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrgent { get; set; }
}