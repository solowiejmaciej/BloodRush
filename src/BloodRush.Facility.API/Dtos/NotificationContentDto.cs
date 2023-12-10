using BloodRush.Contracts.Enums;

namespace BloodRush.DonationFacility.API.Dtos;

public class NotificationContentDto
{
    public string? Title { get; set; }
    public required string Content { get; set; }
    public required ENotificationType NotificationType { get; set; }
}