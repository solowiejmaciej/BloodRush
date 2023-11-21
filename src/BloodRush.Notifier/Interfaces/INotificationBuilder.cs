using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;

namespace BloodRush.Notifier.Interfaces;

public interface INotificationBuilder
{
    Task<Notification> BuildAsync(SendNotificationEvent notificationEvent);
}