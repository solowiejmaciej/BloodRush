using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using FluentValidation;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationsRepository _notificationsRepository;

    public GetNotificationsQueryHandler(
        INotificationsRepository notificationsRepository
        )
    {
        _notificationsRepository = notificationsRepository;
    }
    public async Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _notificationsRepository.GetNotificationsAsync(request.DonorId);
    }
}


public record GetNotificationsQuery : IRequest<List<NotificationDto>>
{
    public Guid DonorId { get; set; }
}

public class GetNotificationsQueryValidator : AbstractValidator<GetNotificationsQuery>
{
    public GetNotificationsQueryValidator()
    {
        RuleFor(x => x.DonorId).NotEmpty();
    }
}