using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using Authentication.Application.Domain.Plugins.Cryptography;
using Authentication.Application.Mediator.Commands.Auth.PostFlowLogin;

namespace Authentication.Application.Mediator.Commands.Auth.PostLogin;

public class PostLoginCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService,
    IPasswordHash passwordHash) : BaseHandler(serviceProvider), IRequestHandler<PostLoginCommand, Result>
{
    public async Task<Result> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
           .FirstOrDefaultAsync(
               where: usuario => usuario.Email == request.Email,
               cancellationToken: cancellationToken);

        if (usuario == null || !passwordHash.PasswordIsEquals(request.Senha, usuario.Chave, usuario.Senha))
        {
            return Result.Failure<PostFlowLoginCommandHandler>(UsuarioFailures.EmailSenhaInvalidos);
        }

        var (token, dataExpiracao) = await tokenService.GenerateTokenAsync(
            usuario: usuario,
            cancellationToken: cancellationToken);

        return Result.Ok(new TokenModel
        {
            AccessToken = token,
            ExpireDate = dataExpiracao,
            ExpiresIn = (dataExpiracao - DateTime.Now).Minutes,
        });
    }
}