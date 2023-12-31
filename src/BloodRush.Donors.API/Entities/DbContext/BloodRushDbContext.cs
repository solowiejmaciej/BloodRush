#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Entities.DbContext;

public class BloodRushDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BloodRushDbContext(DbContextOptions<BloodRushDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<Donor> Donors { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

   
}