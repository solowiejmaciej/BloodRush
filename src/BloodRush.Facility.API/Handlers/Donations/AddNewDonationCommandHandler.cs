using AutoMapper;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Entities.Enums;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donations;

public class AddNewDonationCommandHandler : IRequestHandler<AddNewDonationCommand>
{
    private readonly ILogger<AddNewDonationCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDonationRepository _donationRepository;
    private readonly IDonorInfoRepository _donorInfoRepository;
    private readonly IMediator _mediator;

    public AddNewDonationCommandHandler(
        ILogger<AddNewDonationCommandHandler> logger,
        IMapper mapper,
        IDonationRepository donationRepository,
        IDonorInfoRepository donorInfoRepository,
        IMediator mediator
        )
    {
        _logger = logger;
        _mapper = mapper;
        _donationRepository = donationRepository;
        _donorInfoRepository = donorInfoRepository;
        _mediator = mediator;
    }
    public async Task Handle(AddNewDonationCommand request, CancellationToken cancellationToken)
    {
        var donorRestingPeriodInfo = await _donorInfoRepository.GetRestingPeriodInfoByDonorIdAsync(request.DonorId);
        if(donorRestingPeriodInfo.IsRestingPeriodActive) throw new DonorIsRestingException();
        
        var donation = _mapper.Map<Donation>(request);
        
        await _donationRepository.AddNewDonation(donation);
        if (donation.DonationResult == EDonationResult.Success)
        {
            await _mediator.Publish(new SuccessfulDonationEvent
            {
                DonorId = donation.DonorId,
                DonationDate = donation.DonationDate,
                DonationFacilityId = request.DonationFacilityId
            }, cancellationToken);
        }
    }
}

public record AddNewDonationCommand : IRequest
{
    public Guid DonorId { get; set; }
    public int DonationFacilityId { get; set; }
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}