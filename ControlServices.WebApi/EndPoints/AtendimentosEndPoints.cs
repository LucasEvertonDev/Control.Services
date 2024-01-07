using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentos;

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

        atendimentosEndPoint.MapGet("/",
          async ([FromServices] IMediator mediator, [AsParameters] GetAtendimentosQuery getAtendimentosQuery, CancellationToken cancellationToken) =>
               await mediator.SendAsync(getAtendimentosQuery, cancellationToken))
          .Authorization<ResponseDto<PagedResult<AtendimentoModel>>>();

        return app;
    }
}
