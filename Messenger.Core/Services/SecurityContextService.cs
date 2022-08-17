using System.Security.Claims;
using Messenger.db.Bridge;
using Microsoft.AspNetCore.Http;

namespace Messenger.Core.Services;

public class SecurityContextService : ISecurityContext
{
    private readonly IHttpContextAccessor _HttpContextAccessor;

    public SecurityContextService(IHttpContextAccessor httpContextAccessor)
    {
        _HttpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        Claim guidClaim = _HttpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);

        if(guidClaim == null || !Guid.TryParse(guidClaim.Value, out  Guid result))
        {
            throw new Exception("User not found");
        }

        return result;
    }
}