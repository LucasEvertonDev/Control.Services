using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Clientes.PutCliente;
public class PutClienteCommand : IRequest<Result>, IHandler<PutClienteCommandHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    [FromBody]
    public ClienteEdit Body { get; set; }
}

public class ClienteEdit
{
    public DateTime? DataNascimento { get; set; }

    public string Nome { get; set; }

    public string CPF { get; set; }

    public string Telefone { get; set; }

    public int Situacao { get; set; }
}