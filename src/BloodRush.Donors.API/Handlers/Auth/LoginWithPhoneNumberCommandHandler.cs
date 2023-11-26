#region

using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers.Auth;

public class LoginWithPhoneNumberCommandHandler : IRequestHandler<LoginWithPhoneNumberCommand, LoginResult>
{
    private readonly ILoginManager _loginManager;

    public LoginWithPhoneNumberCommandHandler(
        ILoginManager loginManager
    )
    {
        _loginManager = loginManager;
    }

    public async Task<LoginResult> Handle(LoginWithPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        return await _loginManager.LoginWithPhoneNumberAsync(request.PhoneNumber, request.Password);
    }
}

public record LoginWithPhoneNumberCommand : IRequest<LoginResult>
{
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
}

public class LoginWithPhoneNumberCommandValidator : AbstractValidator<LoginWithPhoneNumberCommand>
{
    public LoginWithPhoneNumberCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}