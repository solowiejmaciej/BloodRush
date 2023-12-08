using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donors;

public class GetDonorPictureQueryHandler : IRequestHandler<GetDonorPictureQuery, Stream>
{
    public async Task<Stream> Handle(GetDonorPictureQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public record GetDonorPictureQuery : IRequest<Stream>
{
    public Guid DonorId { get; init; }
}