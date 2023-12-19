using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donations;

public class GetDonationsQueryHandler : IRequestHandler<GetDonationsQuery, List<DonationDto>>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IMapper _mapper;

    public GetDonationsQueryHandler(
        IDonationRepository donationRepository,
        IMapper mapper
        )
    {
        _donationRepository = donationRepository;
        _mapper = mapper;
    }
    public async Task<List<DonationDto>> Handle(GetDonationsQuery request, CancellationToken cancellationToken)
    {
        var donations = await _donationRepository.GetDonationsByDonorIdAsync(request.DonorId);
        return _mapper.Map<List<DonationDto>>(donations);
    }
    
}

public record GetDonationsQuery : IRequest<List<DonationDto>>
{
    public Guid DonorId { get; set; }
}