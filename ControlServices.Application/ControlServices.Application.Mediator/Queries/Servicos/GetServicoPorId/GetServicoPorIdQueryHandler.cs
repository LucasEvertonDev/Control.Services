using ControlServices.Application.Domain.Contexts.DbAuth.Servicos.Results;

namespace ControlServices.Application.Mediator.Queries.Servicos.GetServicoPorId;
public class GetServicoPorIdQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetServicoPorIdQuery, Result>
{
    public async Task<Result> Handle(GetServicoPorIdQuery request, CancellationToken cancellationToken)
    {
        var servico = await UnitOfWork.ServicoRepository.FirstOrDefaultAsync<ServicoModel>(
            where: servico => servico.Id == request.Id,
            cancellationToken: cancellationToken);

        return Result.Ok(servico);
    }
}
