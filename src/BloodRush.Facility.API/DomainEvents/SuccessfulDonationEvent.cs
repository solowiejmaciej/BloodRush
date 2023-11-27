using MediatR;

namespace BloodRush.DonationFacility.API.DomainEvents;

public class SuccessfulDonationEvent : INotification
{
    public required Guid DonorId { get; set; }
    public required DateTime DonationDate { get; set; }
    public required int DonationFacilityId { get; set; }
}