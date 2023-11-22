using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Auth;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResult>
{
    private readonly ILoginManager _loginManager;

    public RefreshTokenCommandHandler(
        ILoginManager loginManager
        )
    {
        _loginManager = loginManager;
    }
    public async Task<LoginResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _loginManager.RefreshTokenAsync(request.JwtToken, request.RefreshToken);
    }
}

public record RefreshTokenCommand : IRequest<LoginResult>
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.JwtToken)
            .NotEmpty();
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}