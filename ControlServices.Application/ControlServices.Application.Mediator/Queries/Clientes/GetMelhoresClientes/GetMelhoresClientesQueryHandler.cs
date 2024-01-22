namespace ControlServices.Application.Mediator.Queries.Clientes.GetMelhoresClientesQuery;
public class GetMelhoresClientesQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetMelhoresClientesQuery, Result>
{
    public async Task<Result> Handle(GetMelhoresClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await UnitOfWork.ClienteRepository.GetMelhoresClientes(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

        return Result.Ok(clientes);
    }
}
