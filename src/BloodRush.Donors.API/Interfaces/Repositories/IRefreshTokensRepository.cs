#region

using BloodRush.API.Entities;

#endregion

namespace BloodRush.API.Interfaces.Repositories;

public interface IRefreshTokensRepository
{
    Task<RefreshToken?> GetByValueAsync(string value, CancellationToken cancellationToken = default);
    Task SetUsedAsync(RefreshToken value, CancellationToken cancellationToken = default);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
}