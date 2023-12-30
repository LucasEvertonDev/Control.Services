using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Queries.Servicos.GetServicoPorId;
public class GetServicoPorIdQuery : IRequest<Result>, IHandler<GetServicoPorIdQueryHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }
}
