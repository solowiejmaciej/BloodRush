using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Identity.Client;

namespace BloodRush.API.Extensions;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly ILoginManager _loginManager;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IDonorRepository _donorRepository;
    private readonly IRefreshTokensRepository _refreshTokensRepository;

    public ChangePasswordCommandHandler(
        ILoginManager loginManager, IUserContextAccessor userContextAccessor, IDonorRepository donorRepository, IRefreshTokensRepository refreshTokensRepository)
    {
        _loginManager = loginManager;
        _userContextAccessor = userContextAccessor;
        _donorRepository = donorRepository;
        _refreshTokensRepository = refreshTokensRepository;
    }
    
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);
        _loginManager.HashPassword(donor, request.NewPassword);
        await _donorRepository.UpdateDonorAsync(donor);
        await _refreshTokensRepository.DeleteRefreshTokens(donorId);
    }
}

public record ChangePasswordCommand : IRequest
{
    public required string CurrentPassword { get; init; }
    public required string NewPassword { get; init; }
    public required string ConfirmPassword { get; init; }
}

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required.")
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("New password must be at least 3 characters long.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password is required.")
            .Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match new password.");
    }
}