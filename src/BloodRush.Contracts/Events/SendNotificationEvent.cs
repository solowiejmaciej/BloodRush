#region

using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.Contracts.Events;

public class SendNotificationEvent
{
    public Guid DonorId { get; set; }
    public int CollectionFacilityId { get; set; }
    public ENotificationType NotificationType { get; set; }
}