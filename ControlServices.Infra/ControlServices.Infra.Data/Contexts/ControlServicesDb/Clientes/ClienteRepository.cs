using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Results;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Clientes;
public class ClienteRepository(IServiceProvider serviceProvider) : BaseRepository<ControlServicesDbContext, Cliente>(serviceProvider), IClienteRepository
{
    public async Task<PagedResult<ClienteModel>> GetMelhoresClientes(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await Context.Clientes
            .Include(cliente => cliente.Atendimentos)
            .Where(cliente => cliente.Atendimentos.Any(atendimento => atendimento.Data <= DateTime.Now.AddMonths(-12)))
            .OrderByDescending(cliente => cliente.Atendimentos.Count)
            .PaginationAsync<Cliente, ClienteModel>(pageNumber, pageSize, cancellationToken);
    }
}
