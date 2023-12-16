using BloodRush.DonationFacility.API.Extensions;
using BloodRush.DonationFacility.API.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();


builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddGeneralServiceCollection(configuration);
builder.Services.AddDonationServiceCollectionExtension();
builder.Services.AddBloodNeedServiceCollectionExtension();
builder.Services.AddEventsServiceCollectionExtension(configuration);
builder.Services.AddValidationServiceCollectionExtension();
builder.Services.AddSwaggerServiceCollectionExtension();

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();