using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Domain.Structure.Repositories;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<PagedResult<ClienteModel>> GetMelhoresClientes(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}