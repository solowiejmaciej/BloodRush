#region

using BloodRush.Notifier.Entities.DbContext;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.Notifier.Extensions.Db;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BloodRushNotificationsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("App"));
        });
    }
}