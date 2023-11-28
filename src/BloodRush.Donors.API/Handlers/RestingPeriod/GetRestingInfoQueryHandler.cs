using BloodRush.API.Dtos;
using MediatR;

namespace BloodRush.API.Handlers.RestingPeriod;

public class GetRestingInfoQueryHandler : IRequestHandler<GetRestingPeriodQuery, RestingInfoDto>
{
    public Task<RestingInfoDto> Handle(GetRestingPeriodQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GetRestingPeriodQuery : IRequest<RestingInfoDto>
{
}
