using Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;

namespace Authentication.Application.Mediator.Commands.Usuarios.DeleteUsuario;
public class DeleteUsuarioCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<DeleteUsuarioCommand, Result>
{
    public async Task<Result> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository.FirstOrDefaultAsync(u => u.Id.Equals(request.Id));

        if (usuario == null)
        {
            return Result.Failure<PutUsuarioCommandHandler>(UsuarioFailures.UsuarioInexistente);
        }

        await UnitOfWork.UsuarioRepository.DeleteAsync(usuario);
        return Result.Ok();
    }
}
