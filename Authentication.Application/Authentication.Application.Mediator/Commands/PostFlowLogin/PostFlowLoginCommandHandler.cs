namespace Authentication.Application.Mediator.Commands.PostFlowLogin;

public class PostFlowLoginCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService) : BaseHandler(serviceProvider), IRequestHandler<PostFlowLoginCommand, Result>
{
    public async Task<Result> Handle(PostFlowLoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Email == request.Email
                && usuario.Senha == request.Senha);

        if (usuario == null)
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