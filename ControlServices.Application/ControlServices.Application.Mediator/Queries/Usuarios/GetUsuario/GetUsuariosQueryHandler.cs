namespace ControlServices.Application.Mediator.Queries.Usuarios.GetUsuario;
public class GetUsuariosQueryHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<GetUsuariosQuery, Result>
{
    public async Task<Result> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await UnitOfWork.UsuarioRepository
           .GetUsuariosAsync(email: request.Email,
                             nome: request.Nome,
                             pageNumber: request.PageNumber,
                             pageSize: request.PageSize);

        return Result.Ok(usuarios);
    }
}