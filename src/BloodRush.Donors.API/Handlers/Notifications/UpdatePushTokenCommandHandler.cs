using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Notifications;

public class UpdatePushTokenCommandHandler : IRequestHandler<UpdatePushTokenCommand>
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public UpdatePushTokenCommandHandler(
        INotificationsRepository notificationsRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _notificationsRepository = notificationsRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task Handle(UpdatePushTokenCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        await _notificationsRepository.UpdatePushTokenAsync(donorId, request.PushToken);
    }
}

public record UpdatePushTokenCommand : IRequest
{
    public required string PushToken { get; set;}
}