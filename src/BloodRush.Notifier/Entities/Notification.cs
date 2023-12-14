#region

using System.ComponentModel.DataAnnotations.Schema;
using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.Notifier.Entities;

public class Notification
{
    public Notification()
    {
        NotificationChannelLabel = Enum.GetName(NotificationChannel)!;
        NotificationTypeLabel = Enum.GetName(NotificationType)!;
    }

    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public int CollectionFacilityId { get; set; }
    public ENotificationChannel NotificationChannel { get; set; }
    public string NotificationChannelLabel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ENotificationType NotificationType { get; set; }
    public string NotificationTypeLabel { get; set; } 
    public string? Title { get; set; }
    public required string Message { get; set; }
}