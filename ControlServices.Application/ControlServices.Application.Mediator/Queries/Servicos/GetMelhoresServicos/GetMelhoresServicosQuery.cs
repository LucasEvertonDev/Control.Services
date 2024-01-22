using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Servicos.GetMelhoresServicos;
public class GetMelhoresServicosQuery : IRequest<Result>, IHandler<GetMelhoresServicosQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }
}
