#region

using System.Reflection;
using BloodRush.API.Entities.DbContext;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Extensions;

public static class GeneralServiceCollectionExtension
{
    public static void AddGeneralServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HostOptions>(options =>
        {
            options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            options.ShutdownTimeout = TimeSpan.Zero;
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<BloodRushDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("App"));
        });
    }
}