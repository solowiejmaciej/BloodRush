#region

using BloodRush.API.Extensions;
using BloodRush.API.Interfaces;
using BloodRush.API.Middleware;
using BloodRush.API.Repositories;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddGeneralServiceCollection(configuration);
builder.Services.AddEventsServiceCollectionExtension(configuration);
builder.Services.AddAuthServiceCollectionExtension(configuration);
builder.Services.AddValidationServiceCollectionExtension();
builder.Services.AddSwaggerServiceCollectionExtension();
builder.Services.AddDonationServiceCollectionExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();