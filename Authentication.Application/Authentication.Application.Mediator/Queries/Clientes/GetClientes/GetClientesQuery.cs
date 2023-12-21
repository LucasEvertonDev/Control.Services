using System.ComponentModel;
using Authentication.Application.Mediator.Queries.Usuarios.GetUsuarioQuery;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Queries.Clientes.GetClientes;
public class GetClientesQuery : IRequest<Result>, IHandler<GetUsuariosQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "nome")]
    public string Nome { get; set; }
}