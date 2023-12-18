using System.Text.Json.Serialization;
using Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Commands.Usuarios.DeleteUsuario;
public class DeleteUsuarioCommand : IRequest<Result>, IHandler<PutUsuarioCommandHandler>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }
}
