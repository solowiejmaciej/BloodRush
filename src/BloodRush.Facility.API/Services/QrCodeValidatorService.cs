using BloodRush.Contracts.QrCodes;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;

namespace BloodRush.DonationFacility.API.Services;

public class QrCodeValidatorService : IQrCodeValidatorService
{
    public Guid GetDonorIdFromQrCode(string qrCode)
    {
        var res = QrCode.ReadQrCode(qrCode);
        
        if (res == null)
        {
            throw new InvalidQrCodeException();
        }

        return res.GetDonorId();
    }
}