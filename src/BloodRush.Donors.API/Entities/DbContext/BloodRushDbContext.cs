#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Entities.DbContext;

public class BloodRushDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BloodRushDbContext(DbContextOptions<BloodRushDbContext> options) : base(options)
    {
    }

    public DbSet<Donor> Donors { get; set; }
    public DbSet<DonorRestingPeriodInfo> DonorsRestingPeriodInfo { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Donation> Donations { get; set; }
}