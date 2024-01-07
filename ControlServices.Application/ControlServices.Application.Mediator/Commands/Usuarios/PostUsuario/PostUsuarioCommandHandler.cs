using ControlServices.Application.Domain.Plugins.Cryptography;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommandHandler(
    IServiceProvider serviceProvider,
    IPasswordHash passwordHash) : BaseHandler(serviceProvider), IRequestHandler<PostUsuarioCommand, Result>
{
    public async Task<Result> Handle(PostUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await EmailJaCadastrado(request.Email, cancellationToken))
        {
            return Result.Failure<PostUsuarioCommandHandler>(UsuarioFailures.EmailExistente);
        }

        var chaveHash = passwordHash.GeneratePasswordHash();

        var usuario = new Usuario(
            nome: request.Nome,
            email: request.Email,
            senha: passwordHash.EncryptPassword(request.Senha, chaveHash),
            chave: chaveHash);

        if (usuario.HasFailures())
        {
            return Result.Failure<PostUsuarioCommandHandler>(usuario);
        }

        await UnitOfWork.UsuarioRepository.CreateAsync(
            domain: usuario,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }

    private async Task<bool> EmailJaCadastrado(string email, CancellationToken cancellationToken = default)
    {
        return (await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                where: usuario => usuario.Email == email,
                cancellationToken: cancellationToken)) != null;
    }
}