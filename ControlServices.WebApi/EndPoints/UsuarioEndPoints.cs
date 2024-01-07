using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios.Results;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Mediator.Commands.Usuarios.DeleteUsuario;
using ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;
using ControlServices.Application.Mediator.Commands.Usuarios.PutUsuario;
using ControlServices.Application.Mediator.Queries.Usuarios.GetUsuario;

namespace ControlServices.WebApi.EndPoints;

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