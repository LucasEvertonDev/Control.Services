using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Servicos.PutServico;
public class PutServiceCommand : IRequest<Result>, IHandler<PutServiceCommandHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    [FromBody]
    public ServicoEdit Body { get; set; }
}

public class ServicoEdit
{
    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int Situacao { get; set; }
}