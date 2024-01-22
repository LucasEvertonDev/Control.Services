using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using ControlServices.Application.Mediator.Commands.Atendimentos.PutAtendimento;
using ControlServices.Application.Mediator.Commands.Atendimentos.PutRemarcarAtendimento;
using ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentoPorId;
using ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentos;
using ControlServices.Application.Mediator.Queries.Atendimentos.GetTotalizadores;

namespace ControlServices.WebApi.EndPoints;

public static class AtendimentosEndPoints
{
    public static IEndpointRouteBuilder AddAtendimentosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var atendimentosEndPoint = app.MapGroup(route).WithTags(tag);

        atendimentosEndPoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostAtendimentoCommand postAtendimentoCommand, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(postAtendimentoCommand, cancellationToken))
            .Authorization<ResponseDto>();

        atendimentosEndPoint.MapGet($"/{Params.GetRoute<GetAtendimentosQuery>()}",
          async ([FromServices] IMediator mediator, [AsParameters] GetAtendimentosQuery getAtendimentosQuery, CancellationToken cancellationToken) =>
               await mediator.SendAsync(getAtendimentosQuery, cancellationToken))
          .Authorization<ResponseDto<PagedResult<AtendimentoModel>>>();

        atendimentosEndPoint.MapPut("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutAtendimentoCommand putAtendimentoCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(putAtendimentoCommand, cancellationToken))
            .Authorization<ResponseDto>();

        atendimentosEndPoint.MapPut("remarcar/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutRemarcarAtendimentoCommand putRemarcarAtendimentoCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(putRemarcarAtendimentoCommand, cancellationToken))
            .Authorization<ResponseDto>();

        atendimentosEndPoint.MapGet("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] GetAtendimentoPorIdQuery getAtendimentoPorIdQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getAtendimentoPorIdQuery, cancellationToken))
            .Authorization<ResponseDto<AtendimentoModel>>();

        atendimentosEndPoint.MapGet($"/totalizadores/{Params.GetRoute<GetTotalizadoresQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetTotalizadoresQuery getTotalizadoresQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getTotalizadoresQuery, cancellationToken))
            .Authorization<ResponseDto<TotalizadoresModel>>();

        return app;
    }
}
