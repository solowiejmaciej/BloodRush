#region

using BloodRush.Notifier.Builders;
using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Repositories;
using BloodRush.Notifier.Services;

#endregion

namespace BloodRush.Notifier.Extensions.Notifications;

public static class ServiceCollectionExtension
{
    public static void AddNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationsRepository, NotificationsRepository>();
        services.AddScoped<INotificationBuilder, NotificationBuilder>();
        services.AddScoped<ISender, Sender>();
    }
}