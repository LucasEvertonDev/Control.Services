using Authentication.Infra.Data.Structure.Exceptions;

namespace Authentication.Infra.Data.Structure.Repositories;

public class BaseRepository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity<TEntity>
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly NotificationContext _noticationContext;

    public BaseRepository(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetService<TContext>();
        _noticationContext = serviceProvider.GetService<NotificationContext>();
    }

    protected TContext Context => _context;

    public async Task<TEntity> CreateAsync(TEntity domain, CancellationToken cancellationToken = default)
    {
        VerifyFailures();

        await _context.AddAsync(domain, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return domain;
    }

    public async Task<TEntity> UpdateAsync(TEntity domain, CancellationToken cancellationToken = default)
    {
        VerifyFailures();

        _context.Update(domain);

        await _context.SaveChangesAsync(cancellationToken);

        return domain;
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        VerifyFailures();

        var remove = await _context.Set<TEntity>().Where(where).ToListAsync(cancellationToken);

        _context.Remove(remove);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity[] entidadesParaExcluir, CancellationToken cancellationToken = default)
    {
        VerifyFailures();

        if (entidadesParaExcluir != null)
        {
            for (var index = 0; index < entidadesParaExcluir.Count(); index++)
            {
                _context.Remove(entidadesParaExcluir[index]);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TEntity> FirstOrDefaultTrackingAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(where, cancellationToken);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(where, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(where).ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(where).PaginationAsync(pageNumber, pageSize, cancellationToken);
    }

    public virtual IQueryable<TEntity> AsQueriableTracking(CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>().AsTracking();
    }

    public virtual IQueryable<TEntity> AsQueriable(CancellationToken cancellationToken = default)
    {
        return _context.Set<TEntity>();
    }

    protected void VerifyFailures()
    {
        if (_noticationContext.HasNotifications)
        {
            throw new DomainWithFailuresException();
        }
    }
}
