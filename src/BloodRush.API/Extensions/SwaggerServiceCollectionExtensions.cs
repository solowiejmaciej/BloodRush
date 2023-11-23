#region

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

#endregion

namespace BloodRush.API.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static void AddSwaggerServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BloodRush", Version = "v1" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });
    }
}