using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Results;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustosXLucro;
public class GetCustosXLucroQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetCustosXLucroQuery, Result>
{
    public async Task<Result> Handle(GetCustosXLucroQuery request, CancellationToken cancellationToken)
    {
        var atendimentos = await UnitOfWork.AtendimentoRepository.ToListAsync(
            where: atendimento => atendimento.Data.Date >= DateTime.Now.Date.AddMonths(-12),
            cancellationToken: cancellationToken);

        var custos = await UnitOfWork.CustoRepository.ToListAsync(
            where: custos => custos.Data.Date >= DateTime.Now.Date.AddMonths(-12),
            cancellationToken: cancellationToken);

        return Result.Ok(CustosXLucroModel.GetCustosPorLucro(
            atendimentos: atendimentos.ToList(),
            custos: custos.ToList()));
    }
}
