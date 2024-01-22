using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Clientes.GetMelhoresClientesQuery;
public class GetMelhoresClientesQuery : IRequest<Result>, IHandler<GetMelhoresClientesQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }
}
