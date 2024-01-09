using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Atendimentos.GetAtendimentoPorId;
public class GetAtendimentoPorIdQuery : IRequest<Result>, IHandler<GetAtendimentoPorIdQueryHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }
}
