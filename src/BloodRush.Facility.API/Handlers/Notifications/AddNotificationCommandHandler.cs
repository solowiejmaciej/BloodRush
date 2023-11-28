using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand>
{
    public Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AddNotificationCommand : IRequest
{
    
}