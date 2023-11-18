#region

using BloodRush.API.Interfaces;
using BloodRush.API.Models.AppSettings;
using BloodRush.API.Services;
using MassTransit;

#endregion

namespace BloodRush.API.Extensions;

public static class EventsServiceCollectionExtension
{
    public static void AddEventsServiceCollectionExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitConfig = new RabbitSettings();
        var rabbitConfigurationSection = configuration.GetSection("RabbitSettings");
        rabbitConfigurationSection.Bind(rabbitConfig);

        services.AddScoped<IEventPublisher, EventPublisher>();

        services.AddMassTransit(mt => mt.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitConfig.Url, "/", c =>
                {
                    c.Username(rabbitConfig.Username);
                    c.Password(rabbitConfig.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        }));
    }
}