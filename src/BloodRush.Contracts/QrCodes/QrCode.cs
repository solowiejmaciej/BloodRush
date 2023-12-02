using System.Text;

namespace BloodRush.Contracts.QrCodes;

//Todo: This needs to be refactored to use a proper encryption algorithm
public class QrCode
{
    private string QrCodeString { get; set; }
    private Guid DonorId { get; set; }
    public readonly long CreatedAt = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
    public QrCode(Guid donorId)
    {
        DonorId = donorId;
        QrCodeString = Encrypt($"{donorId};{CreatedAt};");
    }
    
    public static QrCode? ReadQrCode(string qrCodeString)
    {
        var decryptedQrCodeString = Decrypt(qrCodeString);
        
        if (string.IsNullOrEmpty(decryptedQrCodeString))
        {
            return null;
        }
        
        var donorId = Guid.Parse(decryptedQrCodeString.Split(';')[0]);
        return new QrCode(donorId);
    }
    
    public Guid ReadDonorId()
    {
        return DonorId;
    }
    
    private string Encrypt(string qrCodeString)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(qrCodeString));
    }
    
    private static string Decrypt(string qrCodeString)
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
    
    public sealed override string ToString()
    {
        return QrCodeString;
    }
}