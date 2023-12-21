using Authentication.Application.Domain.Contexts.DbAuth.Clientes;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Structure.Repositories;

namespace Authentication.Application.Domain.Structure.UnitOfWork;

public interface IUnitOfWorkRepos
{
    IUsuarioRepository UsuarioRepository { get; }

    IRepository<Cliente> ClienteRepository { get; }
}
