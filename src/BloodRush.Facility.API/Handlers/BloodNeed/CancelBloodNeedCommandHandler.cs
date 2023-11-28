using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class CancelBloodNeedCommandHandler : IRequestHandler<CancelBloodNeedCommand>
{
    public Task Handle(CancelBloodNeedCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record CancelBloodNeedCommand : IRequest
{
    public required int BloodNeedId { get; init; }
    public required int CollectionFacilityId { get; init; }
}