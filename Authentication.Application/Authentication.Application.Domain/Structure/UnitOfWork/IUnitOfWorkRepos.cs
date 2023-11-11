using Authentication.Application.Domain.Contexts.Usuarios;
using Authentication.Application.Domain.Structure.Repositories;

namespace Authentication.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkRepos
{
    IRepository<Usuario> UsuarioRepository { get; }
}
