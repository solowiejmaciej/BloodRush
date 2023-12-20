using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;
using MediatR;

namespace BloodRush.Notifier.Handlers;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<Notification>>
{
    private readonly INotificationsRepository _notificationsRepository;

    public GetNotificationsQueryHandler(INotificationsRepository notificationsRepository)
    {
        _notificationsRepository = notificationsRepository;
    }
    public async Task<List<Notification>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GetNotificationsQuery : IRequest<List<Notification>>
{
    public Guid DonorId { get; init; }
}