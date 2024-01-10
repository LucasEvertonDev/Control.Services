using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Atendimentos.PutAtendimento;
public class PutAtendimentoCommand : IRequest<Result>, IHandler<PutAtendimentoCommandHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    [FromBody]
    public AtendimentoEdit Body { get; set; }
}

public class AtendimentoEdit
{
    public DateTime Data { get; set; }

    public Guid ClienteId { get; set; }

    public bool ClienteAtrasado { get; set; }

    public decimal ValorAtendimento { get; set; }

    public decimal? ValorPago { get; set; }

    public string ObservacaoAtendimento { get; set; }

    public int Situacao { get; set; }

    public List<MapAtendimentoServicoEditModel> MapAtendimentosServicos { get; set; }
}

public class MapAtendimentoServicoEditModel
{
    public Guid? Id { get; set; }

    public Guid ServicoId { get; set; }

    public decimal ValorCobrado { get; set; }
}