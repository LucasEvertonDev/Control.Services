using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Results;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustoPorId;
public class GetCustoPorIdQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetCustoPorIdQuery, Result>
{
    public async Task<Result> Handle(GetCustoPorIdQuery request, CancellationToken cancellationToken)
    {
        var custo = await UnitOfWork.CustoRepository.FirstOrDefaultAsync<CustoModel>(
            where: custo => custo.Id == request.Id,
            cancellationToken: cancellationToken);

        return Result.Ok(custo);
    }
}
