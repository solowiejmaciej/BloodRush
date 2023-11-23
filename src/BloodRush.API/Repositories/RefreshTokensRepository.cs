#region

using BloodRush.API.Entities;
using BloodRush.API.Entities.DbContext;
using BloodRush.API.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Repositories;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private readonly BloodRushDbContext _dbContext;

    public RefreshTokensRepository(
        BloodRushDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> GetByValueAsync(string value, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == value, cancellationToken);
    }

    public async Task SetUsedAsync(RefreshToken value, CancellationToken cancellationToken = default)
    {
        value.isUsed = true;
        _dbContext.RefreshTokens.Update(value);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}