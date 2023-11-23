#region

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace BloodRush.API.Entities;

public class RefreshToken
{
    [Key] public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool isUsed { get; set; }
    public bool Invalidated { get; set; }
    public Guid DonorId { get; set; }

    [ForeignKey(nameof(DonorId))] public Donor User { get; set; }
}