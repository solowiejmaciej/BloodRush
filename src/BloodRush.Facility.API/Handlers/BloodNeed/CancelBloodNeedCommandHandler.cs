using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class CancelBloodNeedCommandHandler : IRequestHandler<CancelBloodNeedCommand>
{
    private readonly IEventPublisher _eventPublisher;

    public CancelBloodNeedCommandHandler(
        IEventPublisher eventPublisher
        )
    {
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(CancelBloodNeedCommand request, CancellationToken cancellationToken)
    {
        //TODO: Implement logic to cancel blood need and publish events to sent notification to donors
    }
}

public record CancelBloodNeedCommand : IRequest
{
    public required int BloodNeedId { get; init; }
    public required int CollectionFacilityId { get; init; }
}