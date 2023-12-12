namespace BloodRush.Contracts.QrCodes.Interfaces;

public interface IQrCodeGenerator
{
    QrCode GenerateQrCode(Guid donorId);
}