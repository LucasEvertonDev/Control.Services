using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;
using ControlServices.Infra.Data.Contexts.ControlServicesDb;
using ControlServices.Infra.Data.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Infra.Data.Contexts.ControlServicesDb.Usuarios;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControlServices.Infra.Data.Structure.UnitWork;

public class UnitOfWork(
    IServiceProvider serviceProvider,
    ControlServicesDbContext controlServicesDbContext) : IUnitOfWorkTransaction
{
    private IUsuarioRepository _usuarioRepository;
    private List<IDbContextTransaction> transactions;
    private IRepository<Cliente> _clienteRepository;
    private IRepository<Servico> _servicoRepository;
    private IRepository<Custo> _custoRepository;
    private IAtendimentoRepository _atendimentoRepository;
    private IRepository<MapAtendimentoServico> _mapAtendimentoServicoRepository;

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
            _clienteRepository ??= new BaseRepository<ControlServicesDbContext, Cliente>(serviceProvider);

            return _clienteRepository;
        }
    }

    public IRepository<Servico> ServicoRepository
    {
        get
        {
            _servicoRepository ??= new BaseRepository<ControlServicesDbContext, Servico>(serviceProvider);

            return _servicoRepository;
        }
    }

    public IRepository<Custo> CustoRepository
    {
        get
        {
            _custoRepository ??= new BaseRepository<ControlServicesDbContext, Custo>(serviceProvider);
            return _custoRepository;
        }
    }

    public IAtendimentoRepository AtendimentoRepository
    {
        get
        {
            _atendimentoRepository ??= new AtendimentoRepository(serviceProvider);
            return _atendimentoRepository;
        }
    }

    public IRepository<MapAtendimentoServico> MapAtendimentoServicoRepository
    {
        get
        {
            _mapAtendimentoServicoRepository ??= new BaseRepository<ControlServicesDbContext, MapAtendimentoServico>(serviceProvider);
            return _mapAtendimentoServicoRepository;
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
        transactions.Add(await controlServicesDbContext.Database.BeginTransactionAsync(cancellationToken));
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
