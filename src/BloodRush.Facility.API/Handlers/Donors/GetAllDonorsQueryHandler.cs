#region

using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

#endregion

namespace BloodRush.DonationFacility.API.Handlers.Donors;

public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, List<DonorDto>>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllDonorsQueryHandler> _logger;

    public GetAllDonorsQueryHandler(
        IDonorRepository donorRepository,
        IMapper mapper,
        ILogger<GetAllDonorsQueryHandler> logger
    )
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<DonorDto>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
    {
        var donors = await _donorRepository.GetAllDonorsAsync();
        var donorsDto = _mapper.Map<List<DonorDto>>(donors);
        return donorsDto;
    }
}

public record GetAllDonorsQuery : IRequest<List<DonorDto>>;