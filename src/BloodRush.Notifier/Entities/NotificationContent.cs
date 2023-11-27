using BloodRush.Contracts.Enums;

namespace BloodRush.Notifier.Entities;

public class NotificationContent
{
    public int Id { get; set; }
    public int CollectionFacilityId { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; }
    public ENotificationType NotificationType { get; set; }
}