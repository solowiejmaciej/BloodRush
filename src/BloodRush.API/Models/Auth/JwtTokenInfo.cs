namespace BloodRush.API.Models.Auth;

public class JwtTokenInfo
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Guid DonorId { get; set; }
}