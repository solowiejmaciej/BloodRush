using BloodRush.API.Entities;

namespace BloodRush.API.Interfaces;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByValueAsync(string value, CancellationToken cancellationToken = default);
    Task SetUsedAsync(RefreshToken value, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}