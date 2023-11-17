using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Models;
using Authentication.Application.Domain.Plugins.Cryptography;

namespace Authentication.Application.Mediator.Commands.Auth.PostFlowLogin;

public class PostFlowLoginCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService,
    IPasswordHash passwordHash) : BaseHandler(serviceProvider), IRequestHandler<PostFlowLoginCommand, Result>
{
    public async Task<Result> Handle(PostFlowLoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Email == request.Email);

        if (usuario == null || !passwordHash.PasswordIsEquals(request.Senha, usuario.Chave, usuario.Senha))
        {
            return Result.Failure<PostFlowLoginCommandHandler>(UsuarioFailures.EmailSenhaInvalidos);
        }

        var (token, dataExpiracao) = await tokenService.GenerateToken(usuario);

        return Result.Ok(new TokenModel
        {
            AccessToken = token,
            ExpireDate = dataExpiracao,
            ExpiresIn = (dataExpiracao - DateTime.Now).Minutes,
        });
    }
}