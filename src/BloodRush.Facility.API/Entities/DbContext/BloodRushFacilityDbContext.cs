#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.DonationFacility.API.Entities.DbContext;

public class BloodRushFacilityDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BloodRushFacilityDbContext(DbContextOptions<BloodRushFacilityDbContext> options) : base(options)
    {
    }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<DonorRestingPeriodInfo> DonorsRestingPeriodInfo { get; set; }
    public DbSet<DonationFacility> DonationFacilities { get; set; }
    public DbSet<BloodNeed> BloodNeeds { get; set; }
}