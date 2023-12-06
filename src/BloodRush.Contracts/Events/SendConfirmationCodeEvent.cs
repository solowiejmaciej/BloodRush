using BloodRush.Contracts.ConfirmationCodes;

namespace BloodRush.Contracts.Events;

public record SendConfirmationCodeEvent
{
    public SendConfirmationCodeEvent(ConfirmationCode code, Guid donorId)
    {
        Code = code;
        DonorId = donorId;
    }

    public ConfirmationCode Code { get; set; }
    public Guid DonorId { get; set; }
}