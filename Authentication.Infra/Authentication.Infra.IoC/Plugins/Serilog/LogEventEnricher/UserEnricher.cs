using Authentication.Application.Domain.Plugins.JWT.Conts;
using Authentication.Application.Domain.Structure.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Authentication.Infra.IoC.Plugins.Serilog.LogEventEnricher;
public class UserEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
{
    public UserEnricher()
        : this(new HttpContextAccessor())
    {
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "UsuarioId", httpContextAccessor.HttpContext?.User?.Identity.GetUserClaim(JwtUserClaims.Id) ?? "anonymous"));
    }
}
