namespace ControlServices.Application.Mediator.Queries.Servicos.GetMelhoresServicos;
public class GetMelhoresServicosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetMelhoresServicosQuery, Result>
{
    public async Task<Result> Handle(GetMelhoresServicosQuery request, CancellationToken cancellationToken)
    {
        var servicos = await UnitOfWork.ServicoRepository.GetMelhoresServicos(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        return Result.Ok(servicos);
    }
}
