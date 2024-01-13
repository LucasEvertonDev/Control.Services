using System.Text.Json.Serialization;
using ControlServices.Application.Mediator.Commands.Atendimentos.PutRemarcarAtendimento;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Atendimentos.PutDataAtendimento;
public class PutRemarcarAtendimentoCommand : IRequest<Result>, IHandler<PutRemarcarAtendimentoHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    [FromBody]
    public RemarcarAgendamentoModel Body { get; set; }
}

public class RemarcarAgendamentoModel
{
    public DateTimeOffset Data { get; set; }
}