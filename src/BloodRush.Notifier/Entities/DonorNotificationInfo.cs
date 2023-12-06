
using BloodRush.Contracts.Enums;

namespace BloodRush.Notifier.Entities;

public class DonorNotificationInfo
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public ENotificationChannel NotificationChannel { get; set; }
    public string? PushNotificationToken { get; set; }
}