using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustoPorId;
public class GetCustoPorIdQuery : IRequest<Result>, IHandler<GetCustoPorIdQueryHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }
}