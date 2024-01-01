using System.Text.Json.Serialization;
using Authentication.Application.Mediator.Commands.Servicos.PostServico;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Commands.Servicos.PutServico;
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