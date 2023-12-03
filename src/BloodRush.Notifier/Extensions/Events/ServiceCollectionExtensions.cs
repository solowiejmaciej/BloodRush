#region

using BloodRush.Contracts.Events;
using BloodRush.Notifier.Consumers;
using BloodRush.Notifier.Models.AppSettings;
using MassTransit;

#endregion

namespace BloodRush.Notifier.Extensions.Events;

public static class ServiceCollectionExtensions
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConfig = new RabbitSettings();
        var rabbitConfigurationSection = configuration.GetSection("RabbitSettings");
        rabbitConfigurationSection.Bind(rabbitConfig);

        services.AddMassTransit(mt => mt.AddMassTransit(x =>
        {
            x.AddConsumer<DonorCreatedConsumer>(); // Register the consumer
            x.AddConsumer<DonorDeletedConsumer>(); // Register the consumer
            x.AddConsumer<SendNotificationConsumer>(); // Register the consumer
            x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(rabbitConfig.Url, "/", c =>
                {
                    c.Username(rabbitConfig.Username);
                    c.Password(rabbitConfig.Password);
                });
                cfg.ReceiveEndpoint(RabbitQueues.DonorCreated,
                    endpoint => { endpoint.ConfigureConsumer<DonorCreatedConsumer>(context); });
                cfg.ReceiveEndpoint(RabbitQueues.DonorDeleted,
                    endpoint => { endpoint.ConfigureConsumer<DonorDeletedConsumer>(context); });
                cfg.ReceiveEndpoint(RabbitQueues.NotificationsQueue,
                    endpoint => { endpoint.ConfigureConsumer<SendNotificationConsumer>(context); });
            }));
        }));
    }
}