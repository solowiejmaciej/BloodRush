using MediatR;

namespace BloodRush.API.Handlers.Notifications;

public class UpdatePushTokenCommandHandler : IRequestHandler<UpdatePushTokenCommand>
{
    public Task Handle(UpdatePushTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record UpdatePushTokenCommand : IRequest
{
    public required string PushToken { get; set;}
}