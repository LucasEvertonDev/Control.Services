using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using Authentication.Application.Domain.Plugins.JWT.Contants;
using Authentication.Application.Domain.Structure.Extensions;

namespace Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;

public class PostRefreshTokenCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService) : BaseHandler(serviceProvider), IRequestHandler<PostRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(PostRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
            .FirstOrDefaultTrackingAsync(
                where: usuario => usuario.Id == new Guid(Identity.GetUserClaim(JwtUserClaims.Id)),
                cancellationToken: cancellationToken);

        if (usuario == null)
        {
            return Result.Failure<PostRefreshTokenCommandHandler>(UsuarioFailures.NaoFoiPossivelRecuperarUsuarioLogado);
        }

        await UnitOfWork.UsuarioRepository.UpdateAsync(
            domain: usuario.SetUltimoAcesso(),
            cancellationToken: cancellationToken);

        var (token, dataExpiracao) = await tokenService.GenerateTokenAsync(usuario);

        return Result.Ok(new TokenModel
        {
            AccessToken = token,
            ExpireDate = dataExpiracao,
            ExpiresIn = (dataExpiracao - DateTime.Now).Minutes
        });
    }
}