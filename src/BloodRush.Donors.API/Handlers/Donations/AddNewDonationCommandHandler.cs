using AutoMapper;
using BloodRush.API.DomainEvents;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using MediatR;

namespace BloodRush.API.Handlers.Donations;

public class AddNewDonationCommandHandler : IRequestHandler<AddNewDonationCommand>
{
    private readonly ILogger<AddNewDonationCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDonationRepository _donationRepository;
    private readonly IDonorRepository _donorRepository;
    private readonly IMediator _mediator;

    public AddNewDonationCommandHandler(
        ILogger<AddNewDonationCommandHandler> logger,
        IMapper mapper,
        IDonationRepository donationRepository,
        IDonorRepository donorRepository,
        IMediator mediator
        )
    {
        _logger = logger;
        _mapper = mapper;
        _donationRepository = donationRepository;
        _donorRepository = donorRepository;
        _mediator = mediator;
    }
    public async Task Handle(AddNewDonationCommand request, CancellationToken cancellationToken)
    {
        var donorRestingPeriodInfo = await _donorRepository.GetRestingPeriodInfoByDonorIdAsync(request.DonorId);
        if(donorRestingPeriodInfo.IsRestingPeriodActive) throw new DonorIsRestingException();
        
        var donation = _mapper.Map<Donation>(request);
        await _donationRepository.AddNewDonation(donation);
        if (donation.DonationResult == EDonationResult.Success)
        {
            await _mediator.Publish(new SuccessfulDonationEvent
            {
                DonorId = donation.DonorId,
                DonationDate = donation.DonationDate
            }, cancellationToken);
        }
    }
}

public record AddNewDonationCommand : IRequest
{
    public Guid DonorId { get; set; }
    public DateTime DonationDate { get; set; }
    public EDonationResult DonationResult { get; set; }
    public int QuantityInMl { get; set; }
}