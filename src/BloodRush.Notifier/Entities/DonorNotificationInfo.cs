#region

using System.ComponentModel.DataAnnotations.Schema;
using BloodRush.API.Entities.Enums;

#endregion

namespace BloodRush.Notifier.Entities;

public class DonorNotificationInfo
{
    public Guid Id { get; set; }
    public Guid DonorId { get; set; }
    public ENotificationChannel NotificationChannel { get; set; }
    public string? PushNotificationToken { get; set; }
    [NotMapped]
    public string? PhoneNumber { get; set; }
}