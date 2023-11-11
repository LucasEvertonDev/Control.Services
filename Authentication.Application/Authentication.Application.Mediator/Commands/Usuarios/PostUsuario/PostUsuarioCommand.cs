using Authentication.Application.Domain.Structure.Models;

namespace Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommand : IRequest<Result>, IHandler<PostUsuarioCommandHandler>, IValidationAsync
{
    public string Email { get; set; }

    public string Senha { get; set; }
}
