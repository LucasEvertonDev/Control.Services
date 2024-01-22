using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Results;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Servicos;
public class ServicoRepository(IServiceProvider serviceProvider) : BaseRepository<ControlServicesDbContext, Servico>(serviceProvider), IServicoRepository
{
    public async Task<PagedResult<ServicoModel>> GetMelhoresServicos(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Context.Servicos
            .Include(servico => servico.MapAtendimentoServicos)
            .ThenInclude(map => map.Atendimento)
            .Where(servico =>
                servico.MapAtendimentoServicos.ToList().Exists(map => map.Atendimento.Data >= DateTime.Now.AddMonths(-12)))
            .OrderByDescending(servico => servico.MapAtendimentoServicos.Count)
            .PaginationAsync<Servico, ServicoModel>(pageNumber, pageSize, cancellationToken);
    }
}
