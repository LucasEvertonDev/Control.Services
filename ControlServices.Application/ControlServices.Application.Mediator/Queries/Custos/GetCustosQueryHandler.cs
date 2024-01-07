using ControlServices.Application.Domain.Contexts.DbAuth.Custos.Results;

namespace ControlServices.Application.Mediator.Queries.Custos;
public class GetCustosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetCustosQuery, Result>
{
    public async Task<Result> Handle(GetCustosQuery request, CancellationToken cancellationToken)
        {
            var custo = await UnitOfWork.CustoRepository.ToListAsync<CustoModel>(
                pageNumber: 1,
                pageSize: 10,
                where: custo =>
                (request.Data == null || custo.Data.Date == request.Data.GetValueOrDefault().Date)
                && (request.Valor == null || custo.Valor == request.Valor)
                && (string.IsNullOrWhiteSpace(request.Descricao) || custo.Descricao.Contains(request.Descricao)), cancellationToken: cancellationToken);

            return Result.Ok(custo);
        }
}
