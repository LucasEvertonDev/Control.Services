namespace Authentication.Application.Mediator.Commands.PostLogin;

public class PostLoginCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService) : BaseHandler(serviceProvider), IRequestHandler<PostLoginCommand, Result>
{
    public async Task<Result> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Email == request.Email
                && usuario.Email == request.Senha);

        if (usuario == null)
        {
            return Result.Failure<PostLoginCommandHandler>(UsuarioFailures.EmailSenhaInvalidos);
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