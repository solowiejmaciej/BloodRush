#region

using System.Security.Cryptography;
using BloodRush.API.Entities;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.AppSettings;
using BloodRush.API.Repositories;
using BloodRush.API.Services;
using BloodRush.API.UserContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace BloodRush.API.Extensions;

public static class AuthServiceCollectionExtensions
{
    public static void AddAuthServiceCollectionExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        var authSettings = new AuthSettings();
        var authConfigurationSection = configuration.GetSection("AuthSettings");
        authConfigurationSection.Bind(authSettings);

        services.AddScoped<ILoginManager, LoginManager>();
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<IPasswordHasher<Donor>, PasswordHasher<Donor>>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        
        services.Configure<AuthSettings>(authConfigurationSection);
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextAccessor, UserContextAccessorAccessor>();

        
        var rsa = RSA.Create();

        rsa.ImportSubjectPublicKeyInfo(
            Convert.FromBase64String(authSettings.PublicKey),
            out var _
        );
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        
        var refreshTokenValidationParameter = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ValidateLifetime = false
        };
        
        services.AddSingleton(refreshTokenValidationParameter);
        services.AddSingleton(tokenValidationParameters);

        
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = tokenValidationParameters;
        });

    }
}