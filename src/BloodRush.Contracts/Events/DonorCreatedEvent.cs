namespace BloodRush.Contracts.Events;

public class DonorCreatedEvent
{
    public DonorCreatedEvent(Guid donorId)
    {
        DonorId = donorId;
    }
    public Guid DonorId { get; set; }
}