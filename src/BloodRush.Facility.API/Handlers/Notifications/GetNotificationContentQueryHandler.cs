using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class GetNotificationContentQueryHandler : IRequestHandler<GetNotificationContentQuery, List<NotificationContentDto>>
{
    private readonly INotificationsRepository _notificationsRepository;

    public GetNotificationContentQueryHandler(
        INotificationsRepository notificationsRepository
        )
    {
        _notificationsRepository = notificationsRepository;
    }
    public Task<List<NotificationContentDto>> Handle(GetNotificationContentQuery request, CancellationToken cancellationToken)
    {
        var notificationContent = _notificationsRepository.GetNotificationsContent(request.DonationFacilityId);
        return notificationContent;
    }
}

public class GetNotificationContentQuery : IRequest<List<NotificationContentDto>>
{
    public int DonationFacilityId { get; set; }
}