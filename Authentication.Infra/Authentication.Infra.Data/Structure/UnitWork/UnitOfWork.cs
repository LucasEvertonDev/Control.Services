using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Infra.Data.Contexts.DbAuth;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authentication.Infra.Data.Structure.UnitWork;

public class UnitOfWork(
    IServiceProvider serviceProvider,
    DbAuthContext signCiContext) : IUnitOfWorkTransaction
{
    private IRepository<Usuario> _usuarioRepository;

    private List<IDbContextTransaction> transactions;

    public IRepository<Usuario> UsuarioRepository
    {
        get
        {
            _usuarioRepository ??= new BaseRepository<DbAuthContext, Usuario>(serviceProvider);

            return _usuarioRepository;
        }
    }

    public async Task<TRetorno> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        transactions = new ();

        try
        {
            await OpenTransaction();

            TRetorno retorno = await func();

            await CommitTransaction();

            return retorno;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await RollbackTransaction();
            throw;
        }
    }

    private async Task OpenTransaction()
    {
        transactions.Add(await signCiContext.Database.BeginTransactionAsync());
    }

    private async Task CommitTransaction()
    {
        foreach (var transaction in transactions)
        {
            await transaction.CommitAsync();
        }
    }

    private async Task RollbackTransaction()
    {
        foreach (var transaction in transactions)
        {
            await transaction.RollbackAsync();
        }
    }
}
