using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Queries.Servicos.GetServicos;
public class GetServicosQuery : IRequest<Result>, IHandler<GetServicosQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "nome")]
    public string Nome { get; set; }

    [FromQuery(Name = "descricao")]
    public string Descricao { get; set; }

    [FromQuery(Name = "situacao")]
    public int? Situacao { get; set; }
}
