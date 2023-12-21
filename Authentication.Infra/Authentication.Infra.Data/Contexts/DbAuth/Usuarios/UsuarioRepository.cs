using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Results;

namespace Authentication.Infra.Data.Contexts.DbAuth.Usuarios;
public class UsuarioRepository(IServiceProvider serviceProvider) : BaseRepository<DbAuthContext, Usuario>(serviceProvider), IUsuarioRepository
{
    public async Task<PagedResult<UsuarioModel>> GetUsuariosAsync(string email, string nome, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Context.Usuarios
            .Where(usuario => (string.IsNullOrEmpty(email) || usuario.Email.Contains(email))
                && (string.IsNullOrEmpty(nome) || usuario.Nome.Contains(nome)))
            .Select(usuario => new UsuarioModel
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Name = usuario.Nome,
            })
            .PaginationAsync(pageNumber, pageSize, cancellationToken);
    }
}
