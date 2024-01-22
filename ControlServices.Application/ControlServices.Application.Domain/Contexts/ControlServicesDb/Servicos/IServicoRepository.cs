using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Results;
using ControlServices.Application.Domain.Structure.Pagination;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;

public interface IServicoRepository
{
    Task<PagedResult<ServicoModel>> GetMelhoresServicos(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}