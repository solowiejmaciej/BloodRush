using BloodRush.Notifier.Entities;

namespace BloodRush.Notifier.Interfaces;

public interface ISender
{
    Task SendSmsAsync(Notification notification);
    Task SendPushAsync(Notification notification);
}