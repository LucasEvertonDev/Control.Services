using Notification.Extensions;

namespace Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;

public class PutUsuarioCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutUsuarioCommand, Result>
{
    public async Task<Result> Handle(PutUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await EmailJaCadastrado(request.Body.Email))
        {
            return Result.Failure<PutUsuarioCommandHandler>(UsuarioFailures.EmailExistente);
        }

        var usuario = await UnitOfWork.UsuarioRepository.FirstOrDefaultAsync(
            where: u => u.Id.Equals(request.Id),
            cancellationToken: cancellationToken);

        if (usuario == null)
        {
            return Result.Failure<PutUsuarioCommandHandler>(UsuarioFailures.UsuarioInexistente);
        }

        usuario.AtualizaUsuario(
            nome: request.Body.Nome,
            email: request.Body.Email);

        if (usuario.HasFailures())
        {
            return Result.Failure<PutUsuarioCommandHandler>(usuario);
        }

        await UnitOfWork.UsuarioRepository.UpdateAsync(
            domain: usuario,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }

    private async Task<bool> EmailJaCadastrado(string email)
    {
        return (await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Email == email)) != null;
    }
}