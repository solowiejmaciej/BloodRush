#region

using BloodRush.API.Entities;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Models.Responses;
using Microsoft.AspNetCore.Identity;

#endregion

namespace BloodRush.API.Services;

public class LoginManager : ILoginManager
{
    private readonly IDonorRepository _donorRepository;
    private readonly IPasswordHasher<Donor> _passwordHasher;
    private readonly ITokenManager _tokenManager;
    private readonly ILogger<LoginManager> _logger;
    private bool IsSuccess { get; set; } = true;
    private List<string> Errors { get; } = new();

    public LoginManager(
        IDonorRepository donorRepository,
        IPasswordHasher<Donor> passwordHasher,
        ITokenManager tokenManager,
        ILogger<LoginManager> logger
    )
    {
        _donorRepository = donorRepository;
        _passwordHasher = passwordHasher;
        _tokenManager = tokenManager;
        _logger = logger;
    }

    public async Task<LoginResult> LoginWithPhoneNumberAsync(string phoneNumber, string hashedPassword)
    {
        var donor = await _donorRepository.GetDonorByPhoneNumberAsync(phoneNumber);

        if (donor is null)
        {
            AddError("Invalid credentials");
            return FailResult();
        }

        var result = _passwordHasher.VerifyHashedPassword(donor, donor.Password, hashedPassword);
        if (result != PasswordVerificationResult.Success)
        {
            AddError("Invalid credentials");
            return FailResult();
        }

        // if (donor.IsPhoneNumberConfirmed == false)
        // {
        //     AddError("Phone number is not confirmed.");
        //     return FailResult();
        // }


        var tokenInfo = await _tokenManager.GenerateJwtTokenAsync(donor.Id);

        _logger.LogInformation($"Donor with username {phoneNumber} logged in.");

        return new LoginResult
        {
            IsSuccess = true,
            TokenInfo = tokenInfo
        };
    }

    public async Task<LoginResult> LoginEmailAsync(string email, string hashedPassword)
    {
        var donor = await _donorRepository.GetDonorByEmailAsync(email);
        if (donor is null)
        {
            AddError("Invalid credentials");
            return FailResult();
        }
        
        var result = _passwordHasher.VerifyHashedPassword(donor, donor.Password, hashedPassword);
        if (result != PasswordVerificationResult.Success)
        {
            AddError("Invalid credentials");
            return FailResult();
        }
        
        // if (donor.IsEmailConfirmed == false)
        // {
        //     AddError("Email is not confirmed.");
        //     return FailResult();
        // }
        
        var tokenInfo = await _tokenManager.GenerateJwtTokenAsync(donor.Id);
        
        _logger.LogInformation($"Donor with username {email} logged in.");
        
        return new LoginResult
        {
            IsSuccess = true,
            TokenInfo = tokenInfo
        };
    }

    public async Task<LoginResult> RefreshTokenAsync(string jwtToken, string refreshToken)
    {
        var result = await _tokenManager.RefreshTokenAsync(jwtToken, refreshToken);
        if (result is null) return FailResult();

        return new LoginResult
        {
            IsSuccess = true,
            TokenInfo = result
        };
    }

    public Donor HashPassword(Donor donor, string requestPassword)
    {
        var hashedPassword = _passwordHasher.HashPassword(donor, requestPassword);
        donor.Password = hashedPassword;
        return donor;
    }


    private void AddError(string error)
    {
        Errors.Add(error);
    }

    private LoginResult FailResult()
    {
        return new LoginResult
        {
            IsSuccess = false,
            Errors = Errors
        };
    }
}