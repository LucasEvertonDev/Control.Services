using Authentication.Application.Domain.Contexts.DbAuth.Atendimentos.Results;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Domain.Structure.Repositories;

namespace Authentication.Application.Domain.Contexts.DbAuth.Atendimentos;
public interface IAtendimentoRepository : IRepository<Atendimento>
{
    Task<PagedResult<AtendimentoModel>> GetAtendimentos(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task GetAtendimentos2(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
