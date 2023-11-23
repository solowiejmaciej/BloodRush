#region

using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface ISender
{
    Task SendSmsAsync(Notification notification);
    Task SendPushAsync(Notification notification);
}