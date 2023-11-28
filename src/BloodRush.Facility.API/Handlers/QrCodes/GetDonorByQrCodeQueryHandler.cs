using BloodRush.DonationFacility.API.Dtos;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.QrCodes;

public class GetDonorByQrCodeQueryHandler : IRequestHandler<GetDonorByQrCodeQuery, DonorDto?>
{
    public Task<DonorDto?> Handle(GetDonorByQrCodeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GetDonorByQrCodeQuery : IRequest<DonorDto?>
{
    public required string QrCode { get; init; }
}