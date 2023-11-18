#region

using BloodRush.API.Entities.Enums;

#endregion

namespace BloodRush.API.Entities;

public class DonorNotificationInfo
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public ENotificationChannel NotificationChannel { get; set; }
    public string? PushNotificationToken { get; set; }
}