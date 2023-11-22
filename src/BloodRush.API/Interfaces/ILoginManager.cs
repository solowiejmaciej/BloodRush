using BloodRush.API.Entities;
using BloodRush.API.Models.Responses;

namespace BloodRush.API.Interfaces;

public interface ILoginManager
{
    public Task<LoginResult> LoginWithPhoneNumberAsync(string phoneNumber, string hashedPassword);
    public Task<LoginResult> LoginEmailAsync(string email, string hashedPassword);
    Task<LoginResult> RefreshTokenAsync(string jwtToken, string refreshToken);
    public Donor HashPassword(Donor donor, string requestPassword);
}