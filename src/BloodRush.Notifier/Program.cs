#region

using System.Reflection;
using BloodRush.Notifier.Extensions.Db;
using BloodRush.Notifier.Extensions.Events;
using BloodRush.Notifier.Extensions.Notifications;
using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Services;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


builder.Services.AddRabbitMq(configuration);
builder.Services.AddNotifications(configuration);
builder.Services.AddDatabase(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();