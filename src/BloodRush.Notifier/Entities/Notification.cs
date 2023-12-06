#region

using System.ComponentModel.DataAnnotations.Schema;
using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.Notifier.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public int CollectionFacilityId { get; set; }
    public ENotificationChannel NotificationChannel { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; }
    
    [NotMapped]
    public string? PushNotificationToken { get; set; }
    [NotMapped]
    public string? PhoneNumber { get; set; }
    [NotMapped]
    public string? Email { get; set; } = null!;
}