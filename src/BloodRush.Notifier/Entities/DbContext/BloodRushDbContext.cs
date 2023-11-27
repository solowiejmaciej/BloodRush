#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.Notifier.Entities.DbContext;

public class BloodRushNotificationsDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BloodRushNotificationsDbContext(DbContextOptions<BloodRushNotificationsDbContext> options) : base(options)
    {
    }

    public DbSet<DonorNotificationInfo> DonorsNotificationInfo { get; set; }
    public DbSet<NotificationContent> NotificationsContent { get; set; }
}