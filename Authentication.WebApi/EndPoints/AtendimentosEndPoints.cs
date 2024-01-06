using Authentication.Application.Domain.Structure.Models;
using Authentication.Application.Mediator.Commands.Atendimentos.PostAtendimento;

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

        return app;
    }
}
