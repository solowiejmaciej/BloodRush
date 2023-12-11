using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.QrCodes;

public class GetDonorByQrCodeQueryHandler : IRequestHandler<GetDonorByQrCodeQuery, DonorDto?>
{
    private readonly IQrCodeValidatorService _qrCodeValidatorService;
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;

    public GetDonorByQrCodeQueryHandler(
        IQrCodeValidatorService qrCodeValidatorService,
        IDonorRepository donorRepository,
        IMapper mapper
        )
    {
        _qrCodeValidatorService = qrCodeValidatorService;
        _donorRepository = donorRepository;
        _mapper = mapper;
    }

    public async Task<DonorDto?> Handle(GetDonorByQrCodeQuery request, CancellationToken cancellationToken)
    {
        var donorId = _qrCodeValidatorService.GetDonorIdFromQrCode(request.QrCode);
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);
        return _mapper.Map<DonorDto>(donor);
    }
}

public record GetDonorByQrCodeQuery : IRequest<DonorDto?>
{
    public required string QrCode { get; init; }
}