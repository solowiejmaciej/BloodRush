using BloodRush.Contracts.Enums;

namespace BloodRush.DonationFacility.API.Dtos;

public class NotificationDto
{
    public string? Title { get; set; }
    public required string Message { get; set; }
    public required ENotificationChannel NotificationChannel { get; set; }
}