using BloodRush.Contracts.QrCodes.Interfaces;

namespace BloodRush.Contracts.QrCodes.Services;

public class QrCodeGenerator : IQrCodeGenerator
{
    public QrCode GenerateQrCode(Guid donorId)
    {
        return new QrCode(donorId);
    }
}