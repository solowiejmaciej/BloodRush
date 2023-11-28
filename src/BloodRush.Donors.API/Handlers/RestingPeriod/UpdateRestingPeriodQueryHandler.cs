using MediatR;

namespace BloodRush.API.Handlers.RestingPeriod;

public class UpdateRestingPeriodQueryHandler : IRequestHandler<UpdateRestingPeriodQuery>
{
    public Task Handle(UpdateRestingPeriodQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record UpdateRestingPeriodQuery : IRequest;