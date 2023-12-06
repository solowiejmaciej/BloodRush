using BloodRush.API.Entities;
using BloodRush.Contracts.ConfirmationCodes;
using BloodRush.Contracts.Enums;

namespace BloodRush.API.Interfaces;

public interface IConfirmationCodesRepository
{
    public Task<ConfirmationCode> GenerateCodeAsync(ECodeType type, Guid donorId);
    public Task<bool> IsValidCode(Guid donorId, string code, ECodeType type);
}