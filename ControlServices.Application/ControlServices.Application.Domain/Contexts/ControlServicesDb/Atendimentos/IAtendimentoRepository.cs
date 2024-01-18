using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Domain.Structure.Repositories;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
public interface IAtendimentoRepository : IRepository<Atendimento>
{
    Task<Atendimento> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedResult<AtendimentoModel>> GetAtendimentosAsync(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task GetAtendimentos2(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<TotalizadoresModel> GetTotalizadores(DateTime? dataInicio, DateTime? dataFim, CancellationToken cancellationToken = default);
}
