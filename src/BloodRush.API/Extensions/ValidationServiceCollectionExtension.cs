#region

using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.Donors;
using FluentValidation;
using FluentValidation.AspNetCore;

#endregion

namespace BloodRush.API.Extensions;

public static class ValidationServiceCollectionExtension
{
    public static void AddValidationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IValidator<LoginWithEmailCommand>, LoginWithEmailCommandValidator>();
        services.AddScoped<IValidator<LoginWithPhoneNumberCommand>, LoginWithPhoneNumberCommandValidator>();
        services.AddScoped<IValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();

        services.AddScoped<IValidator<AddNewDonorCommand>, AddNewDonorCommandValidator>();
        services.AddScoped<IValidator<DeleteDonorCommand>, DeleteDonorCommandValidator>();
    }
}