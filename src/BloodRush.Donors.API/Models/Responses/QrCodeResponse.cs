using BloodRush.Contracts.QrCodes;

namespace BloodRush.API.Models.Responses;

public record QrCodeResponse(string QrCodeString, Guid DonorId, long CreatedAt)
{
    public string QrCodeString { get; set; } = QrCodeString;
    public Guid DonorId { get; set; } = DonorId;
    public long CreatedAt { get; set; } = CreatedAt;
}