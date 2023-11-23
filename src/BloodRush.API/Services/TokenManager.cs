#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BloodRush.API.Entities;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.AppSettings;
using BloodRush.API.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace BloodRush.API.Services;

public class TokenManager : ITokenManager
{
    private readonly IOptions<AuthSettings> _authSettings;
    private readonly ILogger<TokenManager> _logger;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly TokenValidationParameters _refreshTokenValidationParameters;

    public TokenManager(
        IOptions<AuthSettings> authSettings,
        ILogger<TokenManager> logger,
        IRefreshTokensRepository refreshTokensRepository,
        TokenValidationParameters refreshTokenValidationParameters
    )
    {
        _authSettings = authSettings;
        _logger = logger;
        _refreshTokensRepository = refreshTokensRepository;
        _refreshTokenValidationParameters = refreshTokenValidationParameters;
    }

    public async Task<JwtTokenInfo> GenerateJwtTokenAsync(Guid donorId)
    {
        var expires = DateTime.Now.AddMinutes(_authSettings.Value.DurationInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, donorId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var rsaPrivateKey = _authSettings.Value.Secret;
        using var rsa = RSA.Create();
        rsa.ImportFromPem(rsaPrivateKey);

        var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };

        var jwt = new JwtSecurityToken(
            audience: _authSettings.Value.Issuer,
            issuer: _authSettings.Value.Issuer,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        var refreshToken = await GenerateRefreshTokenAsync(jwt.Id, donorId);

        var response = new JwtTokenInfo
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            IssuedDate = DateTime.Now,
            ExpiresAt = expires,
            DonorId = donorId,
            RefreshToken = refreshToken.Token
        };

        return response;
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(string jit, Guid donorId)
    {
        var refreshToken = new RefreshToken
        {
            JwtId = jit,
            CreationDate = DateTime.Now,
            DonorId = donorId,
            ExpiryDate = DateTime.Now.AddMonths(6),
            isUsed = false,
            Invalidated = false,
            Token = Guid.NewGuid().ToString()
        };
        await _refreshTokensRepository.AddAsync(refreshToken);
        return refreshToken;
    }

    public async Task<JwtTokenInfo?> RefreshTokenAsync(string jwtToken, string refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(jwtToken);

        if (validatedToken is null) return null;

        var storedRefreshToken = await _refreshTokensRepository.GetByValueAsync(refreshToken);

        if (storedRefreshToken is null) return null;

        if (DateTime.Now > storedRefreshToken.ExpiryDate) return null;

        if (storedRefreshToken.Invalidated) return null;

        if (storedRefreshToken.isUsed) return null;

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        if (storedRefreshToken.JwtId != jti) return null;

        storedRefreshToken.isUsed = true;
        await _refreshTokensRepository.SetUsedAsync(storedRefreshToken);

        var donorId = validatedToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return await GenerateJwtTokenAsync(Guid.Parse(donorId));
    }


    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal =
                tokenHandler.ValidateToken(token, _refreshTokenValidationParameters, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}