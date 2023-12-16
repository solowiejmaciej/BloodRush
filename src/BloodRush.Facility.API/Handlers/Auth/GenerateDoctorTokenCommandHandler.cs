using BloodRush.DonationFacility.API.Models.Auth;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Auth;

public class GenerateDoctorTokenCommandHandler : IRequestHandler<GenerateUserTokenCommand, LoginResult>
{
    public Task<LoginResult> Handle(GenerateUserTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GenerateUserTokenCommand : IRequest<LoginResult>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}