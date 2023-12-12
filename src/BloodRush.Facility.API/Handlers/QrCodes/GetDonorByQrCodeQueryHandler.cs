using AutoMapper;
using BloodRush.Contracts.QrCodes;
using BloodRush.Contracts.QrCodes.Interfaces;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.QrCodes;

public class GetDonorByQrCodeQueryHandler : IRequestHandler<GetDonorByQrCodeQuery, DonorDto?>
{
    private readonly IQrCodeService _qrCodeService;
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;

    public GetDonorByQrCodeQueryHandler(
        IQrCodeService qrCodeService,
        IDonorRepository donorRepository,
        IMapper mapper
        )
    {
        _qrCodeService = qrCodeService;
        _donorRepository = donorRepository;
        _mapper = mapper;
    }

    public async Task<DonorDto?> Handle(GetDonorByQrCodeQuery request, CancellationToken cancellationToken)
    {
        var donorId = _qrCodeService.GetDonorIdFromQrCode(request.QrCode);
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);
        return _mapper.Map<DonorDto>(donor);
    }
}

public record GetDonorByQrCodeQuery : IRequest<DonorDto?>
{
    public required string QrCode { get; init; }
}