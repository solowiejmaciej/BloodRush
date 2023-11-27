#region

using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Models.AppSettings;
using BloodRush.DonationFacility.API.Services;
using MassTransit;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

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