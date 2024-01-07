using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using ControlServices.Application.Domain.Plugins.Cryptography;

namespace ControlServices.Application.Mediator.Commands.Auth.PostFlowLogin;

public class PostFlowLoginCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService,
    IPasswordHash passwordHash) : BaseHandler(serviceProvider), IRequestHandler<PostFlowLoginCommand, Result>
{
    public async Task<Result> Handle(PostFlowLoginCommand request, CancellationToken cancellationToken)
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