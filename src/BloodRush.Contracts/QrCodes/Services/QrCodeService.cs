using System.Text;
using BloodRush.Contracts.QrCodes.Exceptions;
using BloodRush.Contracts.QrCodes.Interfaces;
using BloodRush.Contracts.QrCodes.Options;

namespace BloodRush.Contracts.QrCodes.Services;

public class QrCodeService : IQrCodeService
{
    
    private readonly QrCodeOptions _qrCodeOptions;

    public QrCodeService(QrCodeOptions qrCodeOptions)
    {
        _qrCodeOptions = qrCodeOptions;
    }
    
    public Guid GetDonorIdFromQrCode(string qrCodeString)
    {
        var decryptedQrCodeString = Decrypt(qrCodeString);
        if (ValidateQrCode(decryptedQrCodeString))
        {
            return Guid.Parse(decryptedQrCodeString.Split(';')[0]);
        }
        
        throw new InvalidQrCodeException();
    }
    
    
    private string Decrypt(string qrCodeString)
    {
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(qrCodeString));
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid QR Code");
            return string.Empty;
        }
    }
    
    private bool ValidateQrCode(string decryptedQrCodeString)
    {
        if (string.IsNullOrEmpty(decryptedQrCodeString))
        {
            return false;
        }

        var qrCodeParts = decryptedQrCodeString.Split(';');
        if (qrCodeParts.Length != 3)
        {
            return false;
        }
        
        var createdAtString = qrCodeParts[1];
        
        if (!long.TryParse(createdAtString, out var createdAtLong))
        {
            return false;
        }
        
        var nowInLong = DateTimeOffset.Now.ToUnixTimeSeconds();
        
        var expirationTimeInLong = DateTimeOffset.FromUnixTimeSeconds(createdAtLong)
            .AddSeconds(_qrCodeOptions.LifeTimeInSeconds)
            .ToUnixTimeSeconds();
        
        if (nowInLong > expirationTimeInLong)
        {
            return false;
        }
        
        return true;
    }

}