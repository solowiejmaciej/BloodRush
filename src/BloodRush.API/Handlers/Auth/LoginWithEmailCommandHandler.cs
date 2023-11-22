using BloodRush.API.Models.Responses;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Auth;

public class LoginWithEmailCommandHandler : IRequestHandler<LoginWithEmailCommand, LoginResult>
{
    public Task<LoginResult> Handle(LoginWithEmailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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