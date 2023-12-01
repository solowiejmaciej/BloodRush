using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.Enums;

namespace BloodRush.API.Dtos;

public class NotificationDto
{
    public string? Title { get; set; }
    public required string Message { get; set; }
    public required ENotificationChannel NotificationChannel { get; set; }
    public required ENotificationType NotificationType { get; set; }
}