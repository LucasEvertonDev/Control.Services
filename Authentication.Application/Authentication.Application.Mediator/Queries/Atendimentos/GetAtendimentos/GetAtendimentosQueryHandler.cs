namespace Authentication.Application.Mediator.Queries.Atendimentos.GetAtendimentos;
public class GetAtendimentosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetAtendimentosQuery, Result>
{
    public async Task<Result> Handle(GetAtendimentosQuery request, CancellationToken cancellationToken)
    {
        var atendimentos = await UnitOfWork.AtendimentoRepository.GetAtendimentos(
            dataInicio: null,
            dataFim: null,
            clienteId: null,
            pageNumber: 1,
            pageSize: 100,
            cancellationToken: cancellationToken);

        return Result.Ok(atendimentos);
    }
}
