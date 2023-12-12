#region

using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using BloodRush.Contracts.QrCodes;
using BloodRush.Contracts.QrCodes.Interfaces;
using BloodRush.Contracts.QrCodes.Options;
using BloodRush.Contracts.QrCodes.Services;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Repositories;
using BloodRush.DonationFacility.API.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

public static class GeneralServiceCollectionExtension
{
    public static void AddGeneralServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
        services.AddScoped<IDonorInfoRepository, DonorInfoRepository>();
        services.AddScoped<IDonorRepository, DonorRepository>();
        services.AddScoped<INotificationsRepository, NotificationsRepository>();
        services.AddScoped<IDonationFacilityRepository, DonationFacilityRepository>();
        
        var qrCodeOptions = configuration.GetSection("QrCodeOptions").Get<QrCodeOptions>();
        services.AddScoped<IQrCodeService, QrCodeService>();
        services.AddSingleton<QrCodeOptions>(qrCodeOptions);

        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<BloodRushFacilityDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}