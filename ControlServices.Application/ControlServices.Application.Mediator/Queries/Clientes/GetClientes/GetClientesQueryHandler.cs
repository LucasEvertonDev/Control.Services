using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
using ControlServices.Application.Domain.Structure.Enuns;

namespace ControlServices.Application.Mediator.Queries.Clientes.GetClientes;
public class GetClientesQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetClientesQuery, Result>
{
    public async Task<Result> Handle(GetClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await UnitOfWork.ClienteRepository
            .ToListAsync<ClienteModel>(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                where: cliente =>
                    (string.IsNullOrWhiteSpace(request.Nome) || cliente.Nome.Contains(request.Nome))
                    && (string.IsNullOrWhiteSpace(request.Cpf) || cliente.Cpf.Contains(request.Cpf))
                    && (request.Situacao.GetValueOrDefault() == 0 || cliente.Situacao == (Situacao)request.Situacao.GetValueOrDefault()),
                cancellationToken: cancellationToken);

        return Result.Ok(clientes);
    }
}
