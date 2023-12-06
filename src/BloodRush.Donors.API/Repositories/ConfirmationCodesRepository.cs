#region

using BloodRush.API.Entities;
using BloodRush.API.Interfaces;
using BloodRush.Contracts.ConfirmationCodes;
using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.API.Repositories;

public class ConfirmationCodesRepository : IConfirmationCodesRepository
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<ConfirmationCodesRepository> _logger;

    public ConfirmationCodesRepository(
        ICacheService cacheService,
        ILogger<ConfirmationCodesRepository> logger
    )
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<ConfirmationCode> GenerateCodeAsync(ECodeType channel, Guid userId)
    {
        await _cacheService.RemoveDataAsync($"code:{channel}:{userId}");
        var code = new ConfirmationCode
        {
            CodeType = channel
        };
        await _cacheService.SetDataAsync($"code:{channel}:{userId}", code, code.ExpiresAt);
        return code;
    }

    public async Task<bool> IsValidCode(Guid userId, string code, ECodeType channel)
    {
        var cachedCode = await _cacheService.GetDataAsync<ConfirmationCode>($"code:{channel}:{userId}");
        if (cachedCode == null)
        {
            _logger.LogInformation("Code not found");
            return false;
        }

        if (cachedCode.Code.ToString() != code)
        {
            _logger.LogInformation("Code is invalid");
            return false;
        }

        return true;
    }
    
}