#region

using BloodRush.DonationFacility.API.Handlers.Notifications;
using FluentValidation;
using FluentValidation.AspNetCore;

#endregion

namespace BloodRush.DonationFacility.API.Extensions;

public static class ValidationServiceCollectionExtension
{
    public static void AddValidationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<GetNotificationsQuery>, GetNotificationsQueryValidator>();
    }
}