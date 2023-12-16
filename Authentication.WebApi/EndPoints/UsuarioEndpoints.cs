using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Models;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;

namespace Architecture.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static IEndpointRouteBuilder AddUsuariosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var usuariosEndpoint = app.MapGroup(route).WithTags(tag);

        usuariosEndpoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostUsuarioCommand criarUsuarioCommand) =>
                 await mediator.SendAsync(criarUsuarioCommand))
            .AllowAnonymous<ResponseDto<TokenModel>>();
        return app;
    }
}