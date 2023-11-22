using BloodRush.API.Models.Auth;

namespace BloodRush.API.Models.Responses;

public class LoginResult
{
    public required bool IsSuccess { get; init; }
    public List<string> Errors { get; init; }
    public JwtTokenInfo TokenInfo { get; init; }
}