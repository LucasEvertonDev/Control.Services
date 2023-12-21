using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Mediator.Commands.Usuarios.DeleteUsuario;
using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;
using Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;
using Authentication.Application.Mediator.Queries.Usuarios.GetUsuarioQuerry;

namespace Architecture.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static IEndpointRouteBuilder AddUsuariosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var usuariosEndpoint = app.MapGroup(route).WithTags(tag);

        usuariosEndpoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostUsuarioCommand criarUsuarioCommand, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(criarUsuarioCommand, cancellationToken))
            .AllowAnonymous<ResponseDto<TokenModel>>();

        usuariosEndpoint.MapGet($"/{Params.GetRoute<GetUsuariosQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetUsuariosQuery pegaUsuarioQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(pegaUsuarioQuery, cancellationToken))
            .Authorization<ResponseDto<PagedResult<UsuarioModel>>>();

        usuariosEndpoint.MapPut("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutUsuarioCommand atualizaUsuarioCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(atualizaUsuarioCommand, cancellationToken))
            .Authorization<ResponseDto>();

        usuariosEndpoint.MapDelete("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] DeleteUsuarioCommand deleterUsuarioCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(deleterUsuarioCommand, cancellationToken))
            .Authorization<ResponseDto>();
        return app;
    }
}