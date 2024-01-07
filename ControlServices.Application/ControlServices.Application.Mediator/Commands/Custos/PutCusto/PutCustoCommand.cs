using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Commands.Custos.PutCusto;
public class PutCustoCommand : IRequest<Result>, IHandler<PutCustoCommandHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    public CustoEdit Body { get; set; }
}

public class CustoEdit
{
    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public string Descricao { get; set; }
}
