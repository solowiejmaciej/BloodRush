#region

using System.Data;
using BloodRush.Notifier.Entities.DbContext;
using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.Notifier.Extensions.Db;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
        services.AddScoped<IDonorRepository, DonorRepository>();
        services.AddDbContext<BloodRushNotificationsDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}