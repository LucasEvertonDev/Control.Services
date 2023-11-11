using Authentication.Application.Mediator.Pipelines;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.Mediator;

public static class BootstrapModule
{
    public static void RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BootstrapModule).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

        services.AddValidatorsFromAssemblyContaining<BaseHandler>();
    }
}
