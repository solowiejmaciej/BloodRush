using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public ChangeEmailCommandHandler(
        IDonorRepository donorRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _donorRepository = donorRepository;
        _userContextAccessor = userContextAccessor;
    }
    public async Task Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var currentDonorId = _userContextAccessor.GetDonorId();
        var donor = await _donorRepository.GetDonorByIdAsync(currentDonorId);
        
        var donorWithNewEmail = await _donorRepository.GetDonorByEmailAsync(request.NewEmail);
        if (donorWithNewEmail != null) throw new EmailAlreadyExistsException();
        
        donor.Email = request.NewEmail;
        donor.IsEmailConfirmed = false;
        await _donorRepository.SaveChangesAsync();
    }
}

public record ChangeEmailCommand : IRequest
{
    public required string NewEmail { get; init; }
}

public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .EmailAddress();
    }
}