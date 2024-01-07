using Authentication.Application.Domain.Contexts.DbAuth.Atendimentos.Results;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Mediator.Commands.Atendimentos.PostAtendimento;
using Authentication.Application.Mediator.Queries.Atendimentos.GetAtendimentos;

namespace Authentication.WebApi.EndPoints;

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
