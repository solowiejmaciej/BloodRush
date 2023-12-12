using System.Text;

namespace BloodRush.Contracts.QrCodes;

//Todo: This needs to be refactored to use a proper encryption algorithm
public class QrCode
{
    private string QrCodeString { get; }
    public Guid DonorId { get; set; }
    public readonly long CreatedAt = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
    internal QrCode(Guid donorId)
    {
        DonorId = donorId;
        QrCodeString = Encrypt($"{donorId};{CreatedAt};");
    }
    
    private string Encrypt(string qrCodeString)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(qrCodeString));
    }
    
    public sealed override string ToString()
    {
        return QrCodeString;
    }
}