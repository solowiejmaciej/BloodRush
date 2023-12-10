#region

using BloodRush.DonationFacility.API.Handlers.Notifications;
using BloodRush.DonationFacility.API.Models.Requests;
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
        services.AddScoped<IValidator<AddNotificationCommand>, AddNotificationCommandValidator>();
        services.AddScoped<IValidator<UpdateNotificationContentRequest>, UpdateNotificationContentRequestValidator>();
    }
}