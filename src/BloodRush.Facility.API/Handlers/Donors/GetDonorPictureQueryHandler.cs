using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donors;

public class GetDonorPictureQueryHandler : IRequestHandler<GetDonorPictureQuery, Stream?>
{
    private readonly IDonorRepository _donorRepository;

    public GetDonorPictureQueryHandler(
        IDonorRepository donorRepository
        )
    {
        _donorRepository = donorRepository;
    }
    public async Task<Stream?> Handle(GetDonorPictureQuery request, CancellationToken cancellationToken)
    {
        var donorPicture = await _donorRepository.GetDonorPictureByDonorIdAsync(request.DonorId);
        return donorPicture;
    }
}


public record GetDonorPictureQuery : IRequest<Stream?>
{
    public Guid DonorId { get; init; }
}