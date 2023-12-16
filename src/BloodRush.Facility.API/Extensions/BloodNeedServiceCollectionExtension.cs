using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Services;

namespace BloodRush.DonationFacility.API.Extensions;

public static class BloodNeedServiceCollectionExtension
{
    public static void AddBloodNeedServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddScoped<IBloodNeedService, BloodNeedService>();
    }
}