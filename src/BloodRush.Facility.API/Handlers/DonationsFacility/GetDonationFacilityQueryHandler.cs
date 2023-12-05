using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.DonationsFacility;

public class GetDonationFacilityQueryHandler : IRequestHandler<GetDonationFacilityQuery, List<DonationFacilityDto>>
{
    private readonly IDonationFacilityRepository _donationFacilityRepository;
    private readonly IMapper _mapper;

    public GetDonationFacilityQueryHandler(
        IDonationFacilityRepository donationFacilityRepository,
        IMapper mapper
        )
    {
        _donationFacilityRepository = donationFacilityRepository;
        _mapper = mapper;
    }
    
    public async Task<List<DonationFacilityDto>> Handle(GetDonationFacilityQuery request, CancellationToken cancellationToken)
    {
        var donationFacilities = await _donationFacilityRepository.GetDonationFacilitiesAsync();
        var result = _mapper.Map<List<DonationFacilityDto>>(donationFacilities);
        return result;
    }
}

public record GetDonationFacilityQuery : IRequest<List<DonationFacilityDto>>;