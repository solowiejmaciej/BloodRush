namespace BloodRush.Contracts.QrCodes.Exceptions;

public class InvalidQrCodeException : Exception
{
    public InvalidQrCodeException() : base("Invalid QR Code")
    {
        
    }
}