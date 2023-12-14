#region

using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using BloodRush.Notifier.Models.AppSettings;
using Microsoft.Extensions.Options;
using RestSharp;

#endregion

namespace BloodRush.Notifier.Services;

public class Sender : ISender
{
    private readonly ILogger<Sender> _logger;
    private readonly IOptions<SmsApiSettings> _config;
    private readonly INotificationsRepository _notificationsRepository;
    private readonly IDonorRepository _donorRepository;

    public Sender(
        ILogger<Sender> logger, 
        IOptions<SmsApiSettings> config,
        INotificationsRepository notificationsRepository,
        IDonorRepository donorRepository

    )
    {
        _config = config;
        _notificationsRepository = notificationsRepository;
        _donorRepository = donorRepository;
        _logger = logger;
    }
    
    public async Task SendAsync (Notification notification)
    {
        switch (notification.NotificationChannel)
        {
            case ENotificationChannel.Sms:
                await SendSmsAsync(notification);
                break;
            case ENotificationChannel.Push:
                await SendPushAsync(notification);
                break;
            case ENotificationChannel.Email:
                await SendEmailAsync(notification);
                break;
            default:
                throw new InvalidNotificationChannelException();
        }
    }

    private async Task SendEmailAsync(Notification notification)
    {
        _logger.LogInformation("SendingEmails request");
    }

    private async Task SendSmsAsync(Notification notification)
    {
        var phoneNumber = await _donorRepository.GetPhoneNumberAsync(notification.DonorId);
        var baseUrl = _config.Value.ApiUrl;
        var options = new RestClientOptions(baseUrl);
        var client = new RestClient(options);
        var request = new RestRequest("/sms", Method.Post);

        _logger.LogInformation("Creating request");
        _logger.LogInformation($"sending sms to {phoneNumber}");

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("key", _config.Value.Key);
        request.AddParameter("password", _config.Value.Password);
        request.AddParameter("from", _config.Value.SenderName);
        request.AddParameter("to", phoneNumber);
        request.AddParameter("msg", notification.Message);
        
        var response = await client.ExecuteAsync<ErrorResponse>(request);
        _logger.LogInformation($"Response status code: {response.StatusCode}");
        
        var data = response.Data;
        if (data?.errorCode != null)
        {
            _logger.LogError($"Error sending sms: {data.errorMsg}");
        }
        _logger.LogInformation("Sms sent successfully");
        await _notificationsRepository.AddNotificationAsync(notification);
    }

    private async Task SendPushAsync(Notification notification)
    {
        _logger.LogInformation("Sending Push");
        await _notificationsRepository.AddNotificationAsync(notification);
    }
    
    private class ErrorResponse
    {
        public int? errorCode { get; }
        public string? errorMsg { get; }
    }
}