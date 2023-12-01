using BloodRush.API.Dtos;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Notifications;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetNotificationsQueryHandler(
        INotificationsRepository notificationsRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _notificationsRepository = notificationsRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        return await _notificationsRepository.GetNotificationsAsync(donorId);
    }
}

public record GetNotificationsQuery : IRequest<List<NotificationDto>>;