namespace Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommand : IRequest<Result>, IHandler<PostUsuarioCommandHandler>
{
    public string Email { get; set; }

    public string Senha { get; set; }
}
