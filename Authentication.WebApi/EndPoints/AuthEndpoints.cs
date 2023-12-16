using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Auth.PostLogin;
using Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;

namespace Architecture.WebApi.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder AddAuthEndpoints(this IEndpointRouteBuilder app, string route, string tag)
    {
        var authEndpoints = app.MapGroup(route).WithTags(tag);

        authEndpoints.MapPost("login/",
            async ([FromServices] IMediator mediator, [FromBody] PostLoginCommand request) =>
                 await mediator.SendAsync(request))
            .AllowAnonymous<ResponseDto<TokenModel>>();

        authEndpoints.MapPost("refreshtoken/",
            async ([FromServices] IMediator mediator, PostRefreshTokenCommand refreshTokenCommand) =>
                await mediator.SendAsync(refreshTokenCommand))
            .Authorization<ResponseDto<TokenModel>>("admin");

        authEndpoints.MapPost("flowlogin/",
            async ([FromServices] IMediator mediator, HttpRequest request) =>
                await mediator.SendAsync(await PostLoginCommand.ConvertForm(request), false))
            .AllowAnonymous<TokenModel>();

        return app;
    }
}'