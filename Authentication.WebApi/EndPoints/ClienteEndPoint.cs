using Authentication.Application.Domain.Contexts.DbAuth.Clientes.Results;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Mediator.Commands.Clientes.PostCliente;
using Authentication.Application.Mediator.Commands.Clientes.PutCliente;
using Authentication.Application.Mediator.Queries.Clientes.GeClientesPorId;
using Authentication.Application.Mediator.Queries.Clientes.GetClientes;

namespace Architecture.WebApi.Endpoints;

public static class ClientesEndpoint
{
    public static IEndpointRouteBuilder AddClientesEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var clientesEndpoint = app.MapGroup(route).WithTags(tag);

        clientesEndpoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostClienteCommand postClienteCommand, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(postClienteCommand, cancellationToken))
            .Authorization<ResponseDto>();

        clientesEndpoint.MapGet($"/{Params.GetRoute<GetClientesQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetClientesQuery getClientesQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getClientesQuery, cancellationToken))
            .Authorization<ResponseDto<PagedResult<ClienteModel>>>();

        clientesEndpoint.MapGet("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] GetClientesPorIdQuery getClientesPorId, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getClientesPorId, cancellationToken))
            .Authorization<ResponseDto<ClienteModel>>();

        clientesEndpoint.MapPut("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutClienteCommand putClientCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(putClientCommand, cancellationToken))
            .Authorization<ResponseDto>();

        return app;
    }
}