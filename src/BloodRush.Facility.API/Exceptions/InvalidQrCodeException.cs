namespace BloodRush.DonationFacility.API.Exceptions;

public class InvalidQrCodeException : Exception
{
    public InvalidQrCodeException() : base("Invalid QR Code")
    {
        
    }
}