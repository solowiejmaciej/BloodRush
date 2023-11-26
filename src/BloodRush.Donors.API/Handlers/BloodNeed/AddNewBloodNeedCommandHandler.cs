using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces;
using MediatR;

namespace BloodRush.API.Handlers.BloodNeed;

public class AddNewBloodNeedCommandHandler : IRequestHandler<AddNewBloodNeedCommand>
{
    private readonly IEventPublisher _eventPublisher;

    public AddNewBloodNeedCommandHandler(
        IEventPublisher eventPublisher
        )
    {
        _eventPublisher = eventPublisher;
    }
    public async Task Handle(AddNewBloodNeedCommand request, CancellationToken cancellationToken)
    {
       await _eventPublisher.PublishBloodNeedCreatedEventAsync(request.CollectionFacilityId, request.IsUrget ,cancellationToken);
    }
}

public class AddNewBloodNeedCommand : IRequest
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrget { get; set; }
}