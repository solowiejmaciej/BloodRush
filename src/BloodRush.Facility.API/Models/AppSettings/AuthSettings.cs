namespace BloodRush.DonationFacility.API.Models.AppSettings;

public class AuthSettings
{
    public string Secret { get; set; }
    public string PublicKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }
}