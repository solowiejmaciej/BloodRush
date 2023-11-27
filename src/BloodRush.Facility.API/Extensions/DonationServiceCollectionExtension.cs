#region

using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Repositories;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

public static class DonationServiceCollectionExtension
{
    public static void AddDonationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddScoped<IDonationRepository, DonationRepository>();
    }
}