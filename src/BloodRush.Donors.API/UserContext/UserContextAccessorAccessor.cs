#region

using System.Security.Claims;
using BloodRush.API.Interfaces;

#endregion

namespace BloodRush.API.UserContext;

public class UserContextAccessorAccessor : IUserContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextAccessorAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetDonorId()
    {
        var donorClaims = _httpContextAccessor.HttpContext!.User;

        var donorId = donorClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        return Guid.Parse(donorId);
    }
}