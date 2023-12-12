namespace BloodRush.Contracts.QrCodes.Interfaces;

public interface IQrCodeService
{
    Guid GetDonorIdFromQrCode(string qrCodeString);
}