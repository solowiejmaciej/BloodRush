using MediatR;

namespace BloodRush.DonationFacility.API.DomainEvents;

public class UrgentBloodNeedCreated : INotification
{
    public int BloodNeedId { get; set; }
    public int DonationFacilityId { get; set; }
    public bool IsUrgent { get; set; }
}