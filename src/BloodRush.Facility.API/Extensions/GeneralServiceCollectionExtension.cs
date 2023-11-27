#region

using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using BloodRush.DonationFacility.API.Entities.DbContext;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Repositories;
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
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<BloodRushFacilityDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}