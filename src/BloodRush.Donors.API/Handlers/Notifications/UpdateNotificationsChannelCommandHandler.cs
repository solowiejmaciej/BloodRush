using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Notifications;

public class UpdateNotificationsChannelCommandHandler : IRequestHandler<UpdateNotificationsChannelCommand>
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public UpdateNotificationsChannelCommandHandler(
        INotificationsRepository notificationsRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _notificationsRepository = notificationsRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task Handle(UpdateNotificationsChannelCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        await _notificationsRepository.UpdateNotificationsChannelAsync(donorId, request.Channel);
    }
}

public record UpdateNotificationsChannelCommand : IRequest
{
    public ENotificationChannel Channel { get; set; }
}

public class UpdateNotificationsChannelCommandValidator : AbstractValidator<UpdateNotificationsChannelCommand>
{
    public UpdateNotificationsChannelCommandValidator()
    {
        RuleFor(x => x.Channel).IsInEnum();
    }
}
