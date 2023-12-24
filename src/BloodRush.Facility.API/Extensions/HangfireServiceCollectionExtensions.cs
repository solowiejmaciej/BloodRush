#region

using BloodRush.DonationFacility.API.Hangfire.Manager;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using NotificationService.Hangfire;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

public static class HangfireServiceCollectionExtensions
{
    public static IServiceCollection AddHangfireServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));

        services.AddHangfireServer((serviceProvider, bjsOptions) =>
        {
            bjsOptions.ServerName = "BloodRush.DonationFacility.API";
            bjsOptions.Queues = new[]
            {
                HangfireQueues.HIGH_PRIORITY,
                HangfireQueues.MEDIUM_PRIORITY,
                HangfireQueues.LOW_PRIORITY,
                HangfireQueues.DEFAULT
            };
        });

        services.AddScoped<IJobManager, JobManager>();

        return services;
    }

    public static IApplicationBuilder UseHangfire(this IApplicationBuilder app, IConfiguration configuration)
    {
   
        var hangfireSettings = configuration.GetSection("HangfireSettings");

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "BloodRush.DonationFacility.API",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = hangfireSettings["UserName"],
                    Pass = hangfireSettings["Password"]
                }
            }
        });
        return app;
    }
}