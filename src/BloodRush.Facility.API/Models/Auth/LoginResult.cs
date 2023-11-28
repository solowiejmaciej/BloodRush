#region

#endregion

namespace BloodRush.DonationFacility.API.Models.Auth;

public class LoginResult
{
    public required bool IsSuccess { get; init; }
    public List<string> Errors { get; init; }
    public JwtTokenInfo TokenInfo { get; init; }
}