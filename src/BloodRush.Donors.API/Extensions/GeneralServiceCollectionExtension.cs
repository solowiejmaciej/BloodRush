#region

using System.Data;
using System.Reflection;
using BloodRush.API.Entities.DbContext;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Repositories;
using BloodRush.API.Services;
using BloodRush.Contracts.QrCodes;
using BloodRush.Contracts.QrCodes.Interfaces;
using BloodRush.Contracts.QrCodes.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.API.Extensions;

public static class GeneralServiceCollectionExtension
{
    public static void AddGeneralServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();
        
        services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

        services.AddScoped<IRestingPeriodRepository, RestingPeriodRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<INotificationsRepository, NotificationsRepository>();
        services.AddScoped<IConfirmationCodesRepository, ConfirmationCodesRepository>();
        services.AddScoped<IProfilePictureRepository, ProfilePictureRepository>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<BloodRushDbContext>(options => { options.UseSqlServer(connectionString); });

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<ICacheService, CacheService>();
    }
}