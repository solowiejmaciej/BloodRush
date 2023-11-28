using BloodRush.DonationFacility.API.Models.Auth;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Auth;

public class GenerateDoctorTokenCommandHandler : IRequestHandler<GenerateDoctorTokenCommand, LoginResult>
{
    public Task<LoginResult> Handle(GenerateDoctorTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record GenerateDoctorTokenCommand : IRequest<LoginResult>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}