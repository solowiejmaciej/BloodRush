using MediatR;

namespace BloodRush.API.DomainEvents;

public class SuccessfulDonationEvent : INotification
{
    public required Guid DonorId { get; set; }
    public required DateTime DonationDate { get; set; }
}