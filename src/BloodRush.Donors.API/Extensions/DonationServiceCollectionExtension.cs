#region

using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces;
using BloodRush.API.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

#endregion

namespace BloodRush.API.Extensions;

public static class DonationServiceCollectionExtension
{
    public static void AddDonationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddScoped<IDonationRepository, DonationRepository>();
    }
}