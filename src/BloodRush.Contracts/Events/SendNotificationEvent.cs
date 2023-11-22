using BloodRush.Contracts.Enums;

namespace BloodRush.Contracts.Events;

public class SendNotificationEvent
{
    public Guid DonorId { get; set; }
    public int CollectionFacilityId { get; set; }
    public ENotificationType NotificationType { get; set; }
}