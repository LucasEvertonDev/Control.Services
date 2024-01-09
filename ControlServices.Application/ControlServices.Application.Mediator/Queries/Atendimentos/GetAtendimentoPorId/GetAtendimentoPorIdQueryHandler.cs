using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;

namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentoPorId;
public class GetAtendimentoPorIdQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetAtendimentoPorIdQuery, Result>
{
    public async Task<Result> Handle(GetAtendimentoPorIdQuery request, CancellationToken cancellationToken)
    {
        var atendimento = await UnitOfWork.AtendimentoRepository.FindByIdAsync(
            id: request.Id,
            cancellationToken: cancellationToken);

        return Result.Ok(new AtendimentoModel().FromEntity(atendimento));
    }
}
