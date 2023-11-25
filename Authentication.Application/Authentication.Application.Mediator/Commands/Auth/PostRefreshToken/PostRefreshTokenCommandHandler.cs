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
                usuario => usuario.Id == new Guid(Identity.GetUserClaim(JwtUserClaims.Id)));

        if (usuario == null)
        {
            return Result.Failure<PostRefreshTokenCommandHandler>(UsuarioFailures.NaoFoiPossivelRecuperarUsuarioLogado);
        }

        await UnitOfWork.UsuarioRepository.UpdateAsync(usuario.SetUltimoAcesso());

        var (token, dataExpiracao) = await tokenService.GenerateToken(usuario);

        return Result.Ok(new TokenModel
        {
            AccessToken = token,
            ExpireDate = dataExpiracao,
            ExpiresIn = (dataExpiracao - DateTime.Now).Minutes
        });
    }
}