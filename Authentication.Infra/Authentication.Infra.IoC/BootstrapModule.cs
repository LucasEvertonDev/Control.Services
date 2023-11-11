using Authentication.Application.Domain;
using Authentication.Application.Domain.Plugins.Cryptography;
using Authentication.Application.Domain.Plugins.JWT;
using Authentication.Application.Mediator;
using Authentication.Infra.Data;
using Authentication.Infra.IoC.Plugins.Cryptography;
using Authentication.Infra.IoC.Plugins.JWT.Services;
using Microsoft.Extensions.DependencyInjection;
using Notification;

namespace Authentication.Infra.IoC;

public static class BootstrapModule
{
    public static void AddInfraStructure(this IServiceCollection services, AppSettings configuration)
    {
        services.AddHttpContextAccessor();

        services.RegisterInfraData(configuration);

        services.AddNotification();

        services.RegisterMediatR();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IPasswordHash, PasswordHash>();
    }
}
