#region

using BloodRush.Notifier.Builders;
using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Models.AppSettings;
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
        
        var smsApiSettings = new SmsApiSettings();
        var settings = configuration.GetSection("SmsApiSettings");
        settings.Bind(smsApiSettings);
        
        services.Configure<SmsApiSettings>(settings);
    }
}