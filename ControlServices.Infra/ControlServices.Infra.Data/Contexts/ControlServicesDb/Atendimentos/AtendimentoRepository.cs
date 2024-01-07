using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Results;
using Atendimento = ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Atendimento;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Atendimentos;
public class AtendimentoRepository(IServiceProvider serviceProvider) : BaseRepository<ControlServicesDbContext, Atendimento>(serviceProvider), IAtendimentoRepository
{
    public async Task GetAtendimentos2(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var aux = await (
            from atendimentos in Context.Atendimentos
            join clientes in Context.Clientes
                on atendimentos.ClienteId equals clientes.Id
            where (!dataInicio.HasValue || atendimentos.Data.Date >= dataInicio.Value)
                && (!dataInicio.HasValue || atendimentos.Data.Date <= dataFim.Value)
                && (!clienteId.HasValue || atendimentos.ClienteId == clienteId.Value)
            select new
            {
                atendimentos.Id,
                atendimentos.Data,
                atendimentos.ValorPago,
                Cliente = clientes,
                atendimentos.ClienteAtrasado,
                atendimentos.Situacao,
                atendimentos.ObservacaoAtendimento,
                atendimentos.ValorAtendimento,
                servicos = Context.MapAtendimentosServicos.Where(a => a.AtendimentoId == atendimentos.Id).Include(a => a.Servico).ToList()
            })
            .ToListAsync();

        Console.WriteLine(aux.Count);
    }

    public async Task<PagedResult<AtendimentoModel>> GetAtendimentos(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Context.Atendimentos
            .Where(atendimento => (!dataInicio.HasValue || atendimento.Data.Date >= dataInicio.Value.Date)
                && (!dataInicio.HasValue || atendimento.Data.Date <= dataFim.Value.Date)
                && (!clienteId.HasValue || atendimento.ClienteId == clienteId.Value))
            .Include(atendimento => atendimento.Cliente)
            .Include(atendimento => atendimento.MapAtendimentosServicos)
            .ThenInclude(mapAtendimentoServico => mapAtendimentoServico.Servico)
            .OrderByDescending(atendimento => atendimento.Data.Date)
            .PaginationAsync<Atendimento, AtendimentoModel>(pageNumber, pageSize, cancellationToken);
    }
}
