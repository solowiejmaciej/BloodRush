using BloodRush.DonationFacility.API.Dtos;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
{
    public Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public record GetNotificationsQuery : IRequest<List<NotificationDto>>;