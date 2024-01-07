using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Mediator.Commands.Auth.PostLogin;
using ControlServices.Application.Mediator.Commands.Auth.PostRefreshToken;

namespace Architecture.WebApi.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var authEndpoints = app.MapGroup(route).WithTags(tag);

        authEndpoints.MapPost("login/",
            async ([FromServices] IMediator mediator, [FromBody] PostLoginCommand request, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(request, cancellationToken))
            .AllowAnonymous<ResponseDto<TokenModel>>();

        authEndpoints.MapPost("refreshtoken/",
            async ([FromServices] IMediator mediator, PostRefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(refreshTokenCommand, cancellationToken))
            .Authorization<ResponseDto<TokenModel>>("admin");

        authEndpoints.MapPost("flowlogin/",
            async ([FromServices] IMediator mediator, HttpRequest request, CancellationToken cancellationToken) =>
                await mediator.SendAsync(await PostLoginCommand.ConvertForm(request, cancellationToken), cancellationToken, false))
            .AllowAnonymous<TokenModel>();

        return app;
    }
}