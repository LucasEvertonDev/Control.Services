using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlServices.WebApi.Structure.PolicyProviders;

public class PermissionHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        return Task.CompletedTask;
    }

#pragma warning disable IDE0051 // Remover membros privados não utilizados
#pragma warning disable S1172 // Unused method parameters should be removed
#pragma warning disable S1144 // Unused private types or members should be removed
    private static bool IsOwner(ClaimsPrincipal user, object resource)
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S1172 // Unused method parameters should be removed
#pragma warning restore IDE0051 // Remover membros privados não utilizados
    {
        // Code omitted for brevity
        return true;
    }

#pragma warning disable S1144 // Unused private types or members should be removed
#pragma warning disable S1172 // Unused method parameters should be removed
#pragma warning disable IDE0051 // Remover membros privados não utilizados
    private static bool IsSponsor(ClaimsPrincipal user, object resource)
#pragma warning restore IDE0051 // Remover membros privados não utilizados
#pragma warning restore S1172 // Unused method parameters should be removed
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        // Code omitted for brevity
        return true;
    }
}