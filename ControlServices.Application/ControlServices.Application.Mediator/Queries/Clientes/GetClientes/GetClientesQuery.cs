using System.ComponentModel;
using ControlServices.Application.Mediator.Queries.Usuarios.GetUsuario;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Clientes.GetClientes;
public class GetClientesQuery : IRequest<Result>, IHandler<GetUsuariosQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "cpf")]
    public string Cpf { get; set; }

    [FromQuery(Name = "nome")]
    public string Nome { get; set; }

    [FromQuery(Name = "situacao")]
    public int? Situacao { get; set; }
}