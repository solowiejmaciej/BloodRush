namespace BloodRush.Contracts.Events;

public class DonorCreatedEvent
{
    public DonorCreatedEvent(Guid donorId, string phoneNumber)
    {
        DonorId = donorId;
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; set; }

    public Guid DonorId { get; set; }
}