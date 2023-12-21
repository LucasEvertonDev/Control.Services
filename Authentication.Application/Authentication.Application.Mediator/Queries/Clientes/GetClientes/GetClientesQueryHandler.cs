namespace Authentication.Application.Mediator.Queries.Clientes.GetClientes;
public class GetClientesQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetClientesQuery, Result>
{
    public async Task<Result> Handle(GetClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await UnitOfWork.ClienteRepository
            .ToListAsync(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                where: cliente => (string.IsNullOrWhiteSpace(request.Nome) || cliente.Nome.Contains(request.Nome)),
                cancellationToken: cancellationToken);

        return Result.Ok(clientes);
    }
}
