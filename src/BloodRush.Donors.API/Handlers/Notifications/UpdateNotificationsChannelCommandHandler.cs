using BloodRush.API.Entities.Enums;
using MediatR;

namespace BloodRush.API.Handlers.Notifications;

public class UpdateNotificationsChannelCommandHandler : IRequestHandler<UpdateNotificationsChannelCommand>
{
    public Task Handle(UpdateNotificationsChannelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record UpdateNotificationsChannelCommand : IRequest
{
    public ENotificationChannel Channel { get; set; }
}
