using ControlServices.Application.Mediator.Commands.Usuarios.PutUsuario;

namespace ControlServices.Application.Mediator.Commands.Usuarios.DeleteUsuario;
public class DeleteUsuarioCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<DeleteUsuarioCommand, Result>
{
    public async Task<Result> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository.FirstOrDefaultAsync(
            where: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken);

        if (usuario == null)
        {
            return Result.Failure<PutUsuarioCommandHandler>(UsuarioFailures.UsuarioInexistente);
        }

        await UnitOfWork.UsuarioRepository.DeleteAsync(
            where: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
