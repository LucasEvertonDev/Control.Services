using Authentication.Application.Domain.Contexts.DbAuth.Servicos.Results;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Mediator.Commands.Servicos.PostServico;
using Authentication.Application.Mediator.Commands.Servicos.PutServico;
using Authentication.Application.Mediator.Queries.Servicos.GetServicoPorId;
using Authentication.Application.Mediator.Queries.Servicos.GetServicos;

namespace Authentication.WebApi.EndPoints;

public static class ServicoEndPoints
{
    public static IEndpointRouteBuilder AddServicosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var servicosEndPoint = app.MapGroup(route).WithTags(tag);

        servicosEndPoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostServicoCommand postUsuarioCommand, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(postUsuarioCommand, cancellationToken))
            .AllowAnonymous<ResponseDto>();

        servicosEndPoint.MapGet($"/{Params.GetRoute<GetServicosQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetServicosQuery getServicoQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getServicoQuery, cancellationToken))
            .Authorization<ResponseDto<PagedResult<ServicoModel>>>();

        servicosEndPoint.MapPut("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutServiceCommand putServiceCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(putServiceCommand, cancellationToken))
            .Authorization<ResponseDto>();

        servicosEndPoint.MapGet("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] GetServicoPorIdQuery getServicoPorIdQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getServicoPorIdQuery, cancellationToken))
            .Authorization<ResponseDto<ServicoModel>>();

        return app;
    }
}
