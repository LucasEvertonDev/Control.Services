using ControlServices.Application.Domain;
using ControlServices.Application.Domain.Plugins.Cryptography;
using ControlServices.Application.Domain.Plugins.JWT;
using ControlServices.Application.Mediator;
using ControlServices.Infra.Data;
using ControlServices.Infra.IoC.Plugins.Cryptography;
using ControlServices.Infra.IoC.Plugins.JWT.Services;
using Microsoft.Extensions.DependencyInjection;
using Notification;

namespace ControlServices.Infra.IoC;

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
