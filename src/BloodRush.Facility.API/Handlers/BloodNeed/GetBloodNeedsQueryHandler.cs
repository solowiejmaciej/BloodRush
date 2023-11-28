using BloodRush.DonationFacility.API.Dtos;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class GetBloodNeedsQueryHandler : IRequestHandler<GetBloodNeedsQuery, List<BloodNeedDto>>
{
    public Task<List<BloodNeedDto>> Handle(GetBloodNeedsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public record GetBloodNeedsQuery : IRequest<List<BloodNeedDto>>
{
    public required int CollectionFacilityId { get; init; }
}