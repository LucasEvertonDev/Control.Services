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

    public async Task<TEntity> CreateAsync(TEntity domain)
    {
        VerifyFailures();

        await _context.AddAsync(domain);

        await _context.SaveChangesAsync();

        return domain;
    }

    public async Task<TEntity> UpdateAsync(TEntity domain)
    {
        VerifyFailures();

        _context.Update(domain);

        await _context.SaveChangesAsync();

        return domain;
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        VerifyFailures();

        var remove = await _context.Set<TEntity>().Where(predicate).ToListAsync();

        _context.Remove(remove);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params TEntity[] entidadesParaExcluir)
    {
        VerifyFailures();

        if (entidadesParaExcluir != null)
        {
            for (var index = 0; index < entidadesParaExcluir.Count(); index++)
            {
                _context.Remove(entidadesParaExcluir[index]);
            }
        }

        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> FirstOrDefaultTrackingAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).PaginationAsync(pageNumber, pageSize);
    }

    public virtual IQueryable<TEntity> AsQueriableTracking()
    {
        return _context.Set<TEntity>().AsTracking();
    }

    public virtual IQueryable<TEntity> AsQueriable()
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
