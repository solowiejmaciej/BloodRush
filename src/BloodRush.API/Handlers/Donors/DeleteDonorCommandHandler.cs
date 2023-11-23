#region

using BloodRush.API.Interfaces;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers.Donors;

public class DeleteDonorCommandHandler : IRequestHandler<DeleteDonorCommand, bool>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IDonorRepository _donorRepository;
    private readonly IEventPublisher _eventPublisher;

    public DeleteDonorCommandHandler(
        IUserContextAccessor userContextAccessor,
        IDonorRepository donorRepository,
        IEventPublisher eventPublisher
    )
    {
        _userContextAccessor = userContextAccessor;
        _donorRepository = donorRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<bool> Handle(DeleteDonorCommand request, CancellationToken cancellationToken)
    {
        var currentDonorId = _userContextAccessor.GetDonorId();
        if (currentDonorId != request.DonorId)
            throw new UnauthorizedAccessException("You are not authorized to delete this donor");

        var result = await _donorRepository.DeleteDonorAsync(request.DonorId);

        if (result) await _eventPublisher.PublishDonorDeletedEventAsync(request.DonorId, cancellationToken);

        return result;
    }
}

public record DeleteDonorCommand : IRequest<bool>
{
    public Guid DonorId { get; set; }
}

public class DeleteDonorCommandValidator : AbstractValidator<DeleteDonorCommand>
{
    public DeleteDonorCommandValidator()
    {
        RuleFor(x => x.DonorId)
            .NotEmpty();
    }
}