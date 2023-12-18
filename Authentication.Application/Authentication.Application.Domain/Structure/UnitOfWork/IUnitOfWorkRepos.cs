using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;

namespace Authentication.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkRepos
{
    IUsuarioRepository UsuarioRepository { get; }
}
