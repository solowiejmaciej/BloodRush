using BloodRush.API.Dtos;
using MediatR;

namespace BloodRush.API.Handlers.Donations;
 
public class GetDonationsQueryHandler : IRequestHandler<GetDonationsQuery, List<DonationDto>>
{
    public Task<List<DonationDto>> Handle(GetDonationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GetDonationsQuery : IRequest<List<DonationDto>>
{
    public Guid DonorId { get; init; }
}