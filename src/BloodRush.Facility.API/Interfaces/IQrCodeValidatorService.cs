namespace BloodRush.DonationFacility.API.Interfaces;

public interface IQrCodeValidatorService
{
    Guid GetDonorIdFromQrCode(string qrCode);
}