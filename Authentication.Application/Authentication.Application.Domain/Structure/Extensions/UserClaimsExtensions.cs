using System.Security.Claims;
using System.Security.Principal;

namespace Authentication.Application.Domain.Structure.Extensions;

public static class UserClaimsExtensions
{
    public static string GetUserClaim(this IIdentity identity, string claim)
    {
        if (identity == null)
        {
            return string.Empty;
        }

        var claimsIdentity = identity as ClaimsIdentity;
        return claimsIdentity.FindFirst(claim)?.Value;
    }
}
