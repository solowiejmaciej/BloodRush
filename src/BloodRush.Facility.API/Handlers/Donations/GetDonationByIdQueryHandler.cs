using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donations;

public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, DonationDto>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IMapper _mapper;

    public GetDonationByIdQueryHandler(IDonationRepository donationRepository, IMapper mapper)
    {
        _donationRepository = donationRepository;
        _mapper = mapper;
    }
    public async Task<DonationDto> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
    {
        var donations = await _donationRepository.GetDonationsByDonorIdAsync(request.DonorId);
        var donation = donations.FirstOrDefault(donation => donation.Id == request.DonationId);
        return _mapper.Map<DonationDto>(donation);
    }

}

public record GetDonationByIdQuery : IRequest<DonationDto>
{
    public int DonationId { get; set; }
    public Guid DonorId { get; set; }
}