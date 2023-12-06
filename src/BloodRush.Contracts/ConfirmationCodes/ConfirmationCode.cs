using BloodRush.Contracts.Enums;

namespace BloodRush.Contracts.ConfirmationCodes;

public class ConfirmationCode
{
    public int Code { get; set; } = new Random().Next(100000, 999999);
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.Now.AddMinutes(10);
    public ECodeType CodeType { get; set; }
}

