using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Results;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Domain.Structure.Repositories;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;

public interface IServicoRepository : IRepository<Servico>
{
    Task<PagedResult<ServicoModel>> GetMelhoresServicos(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}