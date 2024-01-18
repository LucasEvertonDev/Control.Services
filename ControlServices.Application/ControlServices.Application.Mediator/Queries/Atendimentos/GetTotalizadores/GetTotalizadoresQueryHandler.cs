namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetTotalizadores;
public class GetTotalizadoresQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetTotalizadoresQuery, Result>
{
    public async Task<Result> Handle(GetTotalizadoresQuery request, CancellationToken cancellationToken)
    {
        var totalizadores = await UnitOfWork.AtendimentoRepository
            .GetTotalizadores(dataInicio: request.DataInicial,
                dataFim: request.DataFinal,
                cancellationToken: cancellationToken);

        return Result.Ok(totalizadores);
    }
}
