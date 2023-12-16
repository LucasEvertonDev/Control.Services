using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.WebApi.Structure.PolicyProviders;

public class DatabasePolicyProvider() : IAuthorizationPolicyProvider
{
    // Quando não tem nada conforme documentação da microsoft só validar se está logado
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
          Task.FromResult(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() =>
        Task.FromResult<AuthorizationPolicy>(null);

    // Injetar o banco e carregar as politicas
    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var authorizationPolicy = new AuthorizationPolicyBuilder();

        authorizationPolicy.RequireRole("admqin");

        return Task.FromResult(authorizationPolicy.Build());
    }
}