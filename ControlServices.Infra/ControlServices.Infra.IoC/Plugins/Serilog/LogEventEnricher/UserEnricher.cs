using ControlServices.Application.Domain.Plugins.JWT.Contants;
using ControlServices.Application.Domain.Structure.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace ControlServices.Infra.IoC.Plugins.Serilog.LogEventEnricher;
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
