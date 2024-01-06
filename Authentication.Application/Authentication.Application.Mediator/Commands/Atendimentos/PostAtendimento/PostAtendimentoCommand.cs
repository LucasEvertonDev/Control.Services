namespace Authentication.Application.Mediator.Commands.Atendimentos.PostAtendimento;
public class PostAtendimentoCommand : IRequest<Result>, IHandler<PostAtendimentoCommandHandler>
{
    public DateTime Data { get; set; }

    public Guid ClienteId { get; set; }

    public bool ClienteAtrasado { get; set; }

    public decimal ValorAtendimento { get; set; }

    public decimal? ValorPago { get; set; }

    public string ObservacaoAtendimento { get; set; }

    public int Situacao { get; set; }

    public List<MapAtendimentoServicoModel> MapAtendimentosServicos { get; set; }
}

public class MapAtendimentoServicoModel
{
    public Guid ServicoId { get; set; }

    public decimal ValorCobrado { get; set; }
}