using BloodRush.API.Dtos;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.RestingPeriod;

public class GetRestingInfoQueryHandler : IRequestHandler<GetRestingPeriodQuery, RestingPeriodDto>
{
    private readonly IRestingPeriodRepository _restingPeriodRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetRestingInfoQueryHandler(
        IRestingPeriodRepository restingPeriodRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _restingPeriodRepository = restingPeriodRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task<RestingPeriodDto> Handle(GetRestingPeriodQuery request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        return await _restingPeriodRepository.GetRestingPeriodByDonorIdAsync(donorId);
    }
}

public record GetRestingPeriodQuery : IRequest<RestingPeriodDto>;
