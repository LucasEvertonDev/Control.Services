using ControlServices.Application.Domain.Contexts.DbAuth.Atendimentos;
using ControlServices.Application.Domain.Contexts.DbAuth.Servicos;

namespace ControlServices.Application.Domain.Contexts.DbAuth.MapAtendimentosServicos;
public class MapAtendimentoServico() : BaseEntity<MapAtendimentoServico>
{
    // Toda vez que se recebe uma entidade deve ter um construtor vazio para o entity entender
    public MapAtendimentoServico(Servico servico, Atendimento atendimento, decimal? valorCobrado)
        : this()
    {
        Ensure(servico).ForContext(map => map.Servico)
            .NotNull(MapAtendimentoServicoFailures.ServicoObrigatorio);

        Ensure(atendimento).ForContext(map => map.Atendimento)
            .NotNull(MapAtendimentoServicoFailures.AtendimentoObrigatorio)
            .NoFailures();

        Servico = servico;
        Atendimento = atendimento;
        ValorCobrado = valorCobrado;
    }

    public Guid ServicoId { get; set; }

    public Guid AtendimentoId { get; set; }

    public decimal? ValorCobrado { get; set; }

    public virtual Servico Servico { get; set; }

    public virtual Atendimento Atendimento { get; set; }
}
