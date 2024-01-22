using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Mediator.Commands.Clientes.PostCliente;
using ControlServices.Application.Mediator.Commands.Clientes.PutCliente;
using ControlServices.Application.Mediator.Queries.Clientes.GeClientesPorId;
using ControlServices.Application.Mediator.Queries.Clientes.GetClientes;
using ControlServices.Application.Mediator.Queries.Clientes.GetMelhoresClientesQuery;

namespace ControlServices.WebApi.EndPoints;

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

        clientesEndpoint.MapGet($"/{Params.GetRoute<GetMelhoresClientesQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetMelhoresClientesQuery getClientesQuery, CancellationToken cancellationToken) =>
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