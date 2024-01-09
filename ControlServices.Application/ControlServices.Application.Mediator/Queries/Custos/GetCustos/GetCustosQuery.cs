using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustos;
public class GetCustosQuery : IRequest<Result>
{
    [FromQuery(Name = "data")]
    public DateTime? Data { get; set; }

    [FromQuery(Name = "valor")]
    public decimal? Valor { get; set; }

    [FromQuery(Name = "descricao")]
    public string Descricao { get; set; }
}
