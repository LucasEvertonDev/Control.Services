using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
using ControlServices.Application.Domain.Structure.Pagination;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;

public interface IClienteRepository
{
    Task<PagedResult<ClienteModel>> GetMelhoresClientes(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}