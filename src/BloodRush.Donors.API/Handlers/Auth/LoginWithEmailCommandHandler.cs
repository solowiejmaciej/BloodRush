#region

using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers.Auth;

public class LoginWithEmailCommandHandler : IRequestHandler<LoginWithEmailCommand, LoginResult>
{
    private readonly ILoginManager _loginManager;

    public LoginWithEmailCommandHandler(ILoginManager loginManager)
    {
        _loginManager = loginManager;
    }
    public async Task<LoginResult> Handle(LoginWithEmailCommand request, CancellationToken cancellationToken)
    {
        return await _loginManager.LoginEmailAsync(request.Email, request.Password);
    }
}

public record LoginWithEmailCommand : IRequest<LoginResult>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class LoginWithEmailCommandValidator : AbstractValidator<LoginWithEmailCommand>
{
    public LoginWithEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}