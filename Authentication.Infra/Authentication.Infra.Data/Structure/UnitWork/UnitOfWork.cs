using Authentication.Application.Domain.Contexts.DbAuth.Clientes;
using Authentication.Application.Domain.Contexts.DbAuth.Custos;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Infra.Data.Contexts.DbAuth;
using Authentication.Infra.Data.Contexts.DbAuth.Usuarios;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authentication.Infra.Data.Structure.UnitWork;

public class UnitOfWork(
    IServiceProvider serviceProvider,
    DbAuthContext dbAuthContext) : IUnitOfWorkTransaction
{
    private IUsuarioRepository _usuarioRepository;
    private List<IDbContextTransaction> transactions;
    private IRepository<Cliente> _clienteRepository;
    private IRepository<Custo> _custoRepository;

    public IUsuarioRepository UsuarioRepository
    {
        get
        {
            _usuarioRepository ??= new UsuarioRepository(serviceProvider);

            return _usuarioRepository;
        }
    }

    public IRepository<Cliente> ClienteRepository
    {
        get
        {
            _clienteRepository ??= new BaseRepository<DbAuthContext, Cliente>(serviceProvider);

            return _clienteRepository;
        }
    }

    public IRepository<Custo> CustoRepository
    {
        get
        {
            _custoRepository ??= new BaseRepository<DbAuthContext, Custo>(serviceProvider);
            return _custoRepository;
        }
    }

    public async Task<TRetorno> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func, CancellationToken cancellationToken = default)
    {
        transactions = [];

        try
        {
            await OpenTransaction(cancellationToken);

            TRetorno retorno = await func();

            await CommitTransaction(cancellationToken);

            return retorno;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await RollbackTransaction(cancellationToken);
            throw;
        }
    }

    private async Task OpenTransaction(CancellationToken cancellationToken = default)
    {
        transactions.Add(await dbAuthContext.Database.BeginTransactionAsync(cancellationToken));
    }

    private async Task CommitTransaction(CancellationToken cancellationToken = default)
    {
        foreach (var transaction in transactions)
        {
            await transaction.CommitAsync(cancellationToken);
        }
    }

    private async Task RollbackTransaction(CancellationToken cancellationToken = default)
    {
        foreach (var transaction in transactions)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}
