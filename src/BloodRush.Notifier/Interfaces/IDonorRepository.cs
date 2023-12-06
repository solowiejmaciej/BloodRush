namespace BloodRush.Notifier.Interfaces;

public interface IDonorRepository
{
    Task<bool> ExistsAsync(Guid donorId);
    Task<string?> GetPhoneNumberAsync(Guid donorId);
    Task<string?> GetEmailAsync(Guid donorId);
}