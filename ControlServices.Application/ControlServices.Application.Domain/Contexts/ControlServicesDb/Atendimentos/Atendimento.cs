using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Enuns;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;

namespace ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;

public class Atendimento() : BasicEntity<Atendimento>
{
    // Toda vez que se recebe uma entidade deve ter um construtor vazio para o entity entender
    public Atendimento(
        DateTime data,
        Cliente cliente,
        bool clienteAtrasado,
        decimal? valorAtendimento,
        decimal? valorPago,
        string observacaoAtendimento,
        SituacaoAtendimento situacao)
        : this()
    {
        Ensure(cliente).ForContext(a => a.Cliente)
            .NotNull(AtendimentoFailures.ClienteObrigatorio)
            .NoFailures();

        Data = data;
        Cliente = cliente;
        ClienteAtrasado = clienteAtrasado;
        ValorAtendimento = valorAtendimento;
        ValorPago = valorPago;
        Situacao = situacao;
        ObservacaoAtendimento = observacaoAtendimento;
        MapAtendimentosServicos = new List<MapAtendimentoServico>();
    }

    public DateTime Data { get; private set; }

    public Guid ClienteId { get; private set; }

    public bool ClienteAtrasado { get; private set; }

    public decimal? ValorAtendimento { get; private set; }

    public decimal? ValorPago { get; private set; }

    public string ObservacaoAtendimento { get; private set; }

    public SituacaoAtendimento Situacao { get; private set; }

    public virtual Cliente Cliente { get; private set; }

    public virtual ICollection<MapAtendimentoServico> MapAtendimentosServicos { get; private set; }

    public void UpdateAtendimento(
        DateTime data,
        Cliente cliente,
        bool clienteAtrasado,
        decimal? valorAtendimento,
        decimal? valorPago,
        string observacaoAtendimento,
        SituacaoAtendimento situacao)
    {
        Ensure(cliente).ForContext(a => a.Cliente)
            .NotNull(AtendimentoFailures.ClienteObrigatorio)
            .NoFailures();

        Data = data;
        Cliente = cliente;
        ClienteAtrasado = clienteAtrasado;
        ValorAtendimento = valorAtendimento;
        ValorPago = valorPago;
        Situacao = situacao;
        ObservacaoAtendimento = observacaoAtendimento;
        MapAtendimentosServicos = new List<MapAtendimentoServico>();
    }

    public void RemarcarAgendamento(DateTime dataAgendamento)
    {
        Ensure(dataAgendamento).ForContext(a => a.Data)
            .NotMinValue(AtendimentoFailures.DataObrigatoria);

        Data = dataAgendamento;
    }

    public void AddServico(MapAtendimentoServico mapAtendimentoServico)
    {
        Ensure(mapAtendimentoServico).ForContext(atendimento => atendimento.MapAtendimentosServicos, MapAtendimentosServicos)
            .NoFailures();

        MapAtendimentosServicos.Add(mapAtendimentoServico);
    }
}
