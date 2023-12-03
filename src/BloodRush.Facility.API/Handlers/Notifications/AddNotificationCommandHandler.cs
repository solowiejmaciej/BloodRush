using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Models.Notifications;
using FluentValidation;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Notifications;

public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand>
{
    private readonly IEventPublisher _eventPublisher;

    public AddNotificationCommandHandler(
        IEventPublisher eventPublisher
        )
    {
        _eventPublisher = eventPublisher;
    }
    public async Task Handle(AddNotificationCommand request, CancellationToken cancellationToken)
    {
        await _eventPublisher.PublishSendNotificationEventAsync(request.DonorId, 1, request.Content ,cancellationToken);
    }
}

public class AddNotificationCommand : IRequest
{
    public Guid DonorId { get; set; }
    public required NotificationContent Content { get; set; }
}

public class AddNotificationCommandValidator : AbstractValidator<AddNotificationCommand>
{
    public AddNotificationCommandValidator()
    {
        RuleFor(x => x.DonorId).NotEmpty();
        RuleFor(x => x.Content.Message)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(200);
    }
}