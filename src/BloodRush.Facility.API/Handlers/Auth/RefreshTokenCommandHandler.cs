using BloodRush.DonationFacility.API.Models.Auth;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Auth;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResult>
{
    public Task<LoginResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record RefreshTokenCommand : IRequest<LoginResult>
{
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}