namespace BloodRush.Contracts.Events;

public class DonorDeletedEvent
{
    public DonorDeletedEvent(Guid donorId)
    {
        DonorId = donorId;
    }

    public Guid DonorId { get; set; }
}