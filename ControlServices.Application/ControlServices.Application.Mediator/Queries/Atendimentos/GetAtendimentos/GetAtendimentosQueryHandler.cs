namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentos;
public class GetAtendimentosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetAtendimentosQuery, Result>
{
    public async Task<Result> Handle(GetAtendimentosQuery request, CancellationToken cancellationToken)
    {
        var atendimentos = await UnitOfWork.AtendimentoRepository.GetAtendimentos(
            dataInicio: request.DataInicial,
            dataFim: request.DataFinal,
            clienteId: request.ClienteId,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        return Result.Ok(atendimentos);
    }
}
