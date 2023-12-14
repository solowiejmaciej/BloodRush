using BloodRush.Contracts.Enums;

namespace BloodRush.DonationFacility.API.Dtos;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public required string Message { get; set; }
    public string NotificationChannelLabel { get; set; }
    public string NotificationTypeLabel { get; set; }
    public DateTime CreatedAt { get; set; }
}