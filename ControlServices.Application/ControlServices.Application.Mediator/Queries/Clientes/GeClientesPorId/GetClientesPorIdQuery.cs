using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Clientes.GeClientesPorId;
public class GetClientesPorIdQuery : IRequest<Result>, IHandler<GetClientesPorIdQueryHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }
}
