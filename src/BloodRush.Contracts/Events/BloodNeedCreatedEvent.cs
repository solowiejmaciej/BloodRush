namespace BloodRush.Contracts.Events;

public class BloodNeedCreatedEvent
{
    public int CollectionFacilityId { get; set; }
    public bool IsUrgent { get; set; }
}