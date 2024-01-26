using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Results;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustosXLucro;
public class GetCustosXLucroQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetCustosXLucroQuery, Result>
{
    public async Task<Result> Handle(GetCustosXLucroQuery request, CancellationToken cancellationToken)
    {
        DateTime dataInicio = Convert.ToDateTime($"{DateTime.Now.AddMonths(-12).Year}-{DateTime.Now.AddMonths(-12).Month}-01");

        var atendimentos = await UnitOfWork.AtendimentoRepository.ToListAsync(
            where: atendimento => atendimento.Data.Date >= dataInicio
                && atendimento.Situacao != Domain.Contexts.ControlServicesDb.Atendimentos.Enuns.SituacaoAtendimento.Cancelado,
            cancellationToken: cancellationToken);

        var custos = await UnitOfWork.CustoRepository.ToListAsync(
            where: custos => custos.Data.Date >= dataInicio,
            cancellationToken: cancellationToken);

        return Result.Ok(CustosXLucroModel.GetCustosPorLucro(
            atendimentos: atendimentos.ToList(),
            custos: custos.ToList()));
    }
}
