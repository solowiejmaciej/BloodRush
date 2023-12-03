#region

using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.Contracts.Events;

public record SendNotificationEvent
{
    public Guid DonorId { get; set; }
    public int CollectionFacilityId { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; }
    public ENotificationType NotificationType { get; set; }
}