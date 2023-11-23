#region

using BloodRush.API.Entities.Enums;

#endregion

namespace BloodRush.Notifier.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public required Guid DonorId { get; set; }
    public required int CollectionFacilityId { get; set; }

    public string? Title { get; set; }
    public required string Message { get; set; }

    public required ENotificationChannel NotificationChannel { get; set; }

    public string? PushNotificationToken { get; set; }
    public string? PhoneNumber { get; set; }
}