using Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;

namespace Authentication.Application.Mediator.Commands.Clientes.PostCliente;
public class PostClienteCommandHandler(
    IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PostUsuarioCommand, Result>
{
    public async Task<Result> Handle(PostUsuarioCommand request, CancellationToken cancellationToken)
    {
        var cliente = await UnitOfWork.ClienteRepository
            .FirstOrDefaultAsync(cliente => cliente.Nome == request.Nome);

        if (cliente == null)
        {
            Console.WriteLine("teste");
        }

        return Result.Ok();
    }
}
