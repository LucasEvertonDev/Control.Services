using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Custos.GetCustos;
public class GetCustosQuery : IRequest<Result>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "datainicial")]
    public DateTime? DataInicial { get; set; }

    [FromQuery(Name = "datafinal")]
    public DateTime? DataFinal { get; set; }

    [FromQuery(Name = "valor")]
    public decimal? Valor { get; set; }

    [FromQuery(Name = "descricao")]
    public string Descricao { get; set; }
}
