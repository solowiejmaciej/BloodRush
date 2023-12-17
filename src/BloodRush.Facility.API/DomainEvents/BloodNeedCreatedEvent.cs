using MediatR;

namespace BloodRush.DonationFacility.API.DomainEvents;

public class BloodNeedCreatedEvent : INotification
{
    public int BloodNeedId { get; set; }
    public int DonationFacilityId { get; set; }
    public bool IsUrgent { get; set; }
}