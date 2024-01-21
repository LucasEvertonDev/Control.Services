using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Results;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustos;
public class GetCustosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetCustosQuery, Result>
{
    public async Task<Result> Handle(GetCustosQuery request, CancellationToken cancellationToken)
    {
        var custo = await UnitOfWork.CustoRepository.ToListAsync<CustoModel>(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            where: custo =>
                (request.DataInicial == null || custo.Data.Date >= request.DataInicial.GetValueOrDefault().Date)
                && (request.DataFinal == null || custo.Data.Date <= request.DataFinal.GetValueOrDefault().Date)
                && (request.Valor == null || custo.Valor == request.Valor)
                && (string.IsNullOrWhiteSpace(request.Descricao) || custo.Descricao.Contains(request.Descricao)),
            cancellationToken: cancellationToken);

        return Result.Ok(custo);
    }
}
