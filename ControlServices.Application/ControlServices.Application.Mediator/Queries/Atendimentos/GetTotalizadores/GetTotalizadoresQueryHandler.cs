namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetTotalizadores;
public class GetTotalizadoresQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetTotalizadoresQuery, Result>
{
    public async Task<Result> Handle(GetTotalizadoresQuery request, CancellationToken cancellationToken)
    {
        DateTime dataInicio = Convert.ToDateTime($"{DateTime.Now.Year}-{DateTime.Now.Month}-01");

        var totalizadores = await UnitOfWork.AtendimentoRepository
            .GetTotalizadores(dataInicio: request.DataInicial ?? dataInicio,
                dataFim: request.DataFinal ?? dataInicio.AddMonths(1).AddSeconds(-1),
                cancellationToken: cancellationToken);

        return Result.Ok(totalizadores);
    }
}
