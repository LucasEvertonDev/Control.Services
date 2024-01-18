using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Enuns;
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

    public async Task<PagedResult<AtendimentoModel>> GetAtendimentosAsync(DateTime? dataInicio, DateTime? dataFim, Guid? clienteId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
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

    public async Task<Atendimento> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Atendimentos
            .Where(atendimento => atendimento.Id == id)
            .Include(atendimento => atendimento.Cliente)
            .Include(atendimento => atendimento.MapAtendimentosServicos)
            .ThenInclude(mapAtendimentoServico => mapAtendimentoServico.Servico)
            .FirstOrDefaultAsync();
    }

    public async Task<TotalizadoresModel> GetTotalizadores(DateTime? dataInicio, DateTime? dataFim, CancellationToken cancellationToken = default)
    {
        var atendimentos = await Context.Atendimentos
            .Where(atendimento => (!dataInicio.HasValue || atendimento.Data.Date >= dataInicio.Value.Date)
                && (!dataInicio.HasValue || atendimento.Data.Date <= dataFim.Value.Date))
            .Include(atendimento => atendimento.MapAtendimentosServicos)
        .ToListAsync();

        var custos = await Context.Custos.Where(custo => (!dataInicio.HasValue || custo.Data.Date >= dataInicio.Value.Date)
                && (!dataInicio.HasValue || custo.Data.Date <= dataFim.Value.Date))
            .ToListAsync(cancellationToken);

        return new TotalizadoresModel()
        {
            Agendados = atendimentos.Count(a => a.Situacao == SituacaoAtendimento.Agendado),
            Concluidos = atendimentos.Count(a => a.Situacao == SituacaoAtendimento.Concluido),
            Lucro = atendimentos.Where(a => a.Situacao == SituacaoAtendimento.Concluido).Sum(a => a.ValorPago.GetValueOrDefault())
                    - custos.Sum(a => a.Valor),
            Receber = atendimentos.Where(a => a.Situacao == SituacaoAtendimento.Concluido).Sum(a => a.ValorAtendimento.GetValueOrDefault())
                    - atendimentos.Where(a => a.Situacao == SituacaoAtendimento.Concluido).Sum(a => a.ValorPago.GetValueOrDefault())
        };
    }
}
