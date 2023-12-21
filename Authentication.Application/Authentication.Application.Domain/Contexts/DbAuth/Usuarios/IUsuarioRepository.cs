using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using Authentication.Application.Domain.Structure.Pagination;
using Authentication.Application.Domain.Structure.Repositories;

namespace Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<PagedResult<UsuarioModel>> GetUsuariosAsync(string email, string nome, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
