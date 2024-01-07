using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios.Results;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Usuarios;
public class UsuarioRepository(IServiceProvider serviceProvider) : BaseRepository<ControlServicesDbContext, Usuario>(serviceProvider), IUsuarioRepository
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
