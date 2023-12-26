#region

using BloodRush.DonationFacility.API.Hangfire.Manager;
using BloodRush.DonationFacility.API.Interfaces;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using NotificationService.Hangfire;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

public static class HangfireServiceCollectionExtensions
{
    public static void AddHangfireServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));

        services.AddHangfireServer((_, bjsOptions) =>
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
    }

    public static void UseHangfire(this IApplicationBuilder app, IConfiguration configuration)
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
    }
}