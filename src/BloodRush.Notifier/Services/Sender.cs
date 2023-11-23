#region

using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;

#endregion

namespace BloodRush.Notifier.Services;

public class Sender : ISender
{
    private readonly ILogger<Sender> _logger;

    public Sender(
        ILogger<Sender> logger
    )
    {
        _logger = logger;
    }

    public async Task SendSmsAsync(Notification notification)
    {
        _logger.LogInformation("Sending SMS");
    }

    public async Task SendPushAsync(Notification notification)
    {
        _logger.LogInformation("Sending Push");
    }
}