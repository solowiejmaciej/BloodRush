using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class UpdateNotificationContentTemplateCommandHandler : IRequestHandler<UpdateNotificationContentTemplateCommand>
{
    private readonly INotificationsRepository _notificationsRepository;

    public UpdateNotificationContentTemplateCommandHandler(
        INotificationsRepository notificationsRepository
        )
    {
        _notificationsRepository = notificationsRepository;
    }
    public async Task Handle(UpdateNotificationContentTemplateCommand request, CancellationToken cancellationToken)
    {
        await _notificationsRepository.UpdateNotificationTemplateContent(request.NotificationType, request.DonationFacilityId, request.Content, request.Title);
    }
}


public record UpdateNotificationContentTemplateCommand : IRequest
{
    public required ENotificationType NotificationType { get; set; }
    public required int DonationFacilityId { get; set; }
    public required string Content { get; set; }
    public string? Title { get; set; }
    
}