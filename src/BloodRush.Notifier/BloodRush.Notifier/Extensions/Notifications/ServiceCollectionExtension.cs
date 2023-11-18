#region

using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Repositories;

#endregion

namespace BloodRush.Notifier.Extensions.Notifications;

public static class ServiceCollectionExtension
{
    public static void AddNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationsRepository, NotificationsRepository>();
    }
}