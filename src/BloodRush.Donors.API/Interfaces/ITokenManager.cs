#region

using BloodRush.API.Entities;
using BloodRush.API.Models.Auth;

#endregion

namespace BloodRush.API.Interfaces;

public interface ITokenManager
{
    public Task<JwtTokenInfo> GenerateJwtTokenAsync(Guid donorId);
    public Task<RefreshToken> GenerateRefreshTokenAsync(string jit, Guid donorId);
    public Task<JwtTokenInfo?> RefreshTokenAsync(string jwtToken, string refreshToken);
}