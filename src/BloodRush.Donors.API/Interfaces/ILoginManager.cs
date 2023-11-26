#region

using BloodRush.API.Entities;
using BloodRush.API.Models.Responses;

#endregion

namespace BloodRush.API.Interfaces;

public interface ILoginManager
{
    Task<LoginResult> LoginWithPhoneNumberAsync(string phoneNumber, string hashedPassword);
    Task<LoginResult> LoginEmailAsync(string email, string hashedPassword);
    Task<LoginResult> RefreshTokenAsync(string jwtToken, string refreshToken);
    Donor HashPassword(Donor donor, string requestPassword);
}