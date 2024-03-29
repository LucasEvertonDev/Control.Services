﻿using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Infra.Data.Structure.Exceptions;

namespace ControlServices.Infra.Data.Structure.Repositories;

public class BaseRepository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : BasicEntity<TEntity>
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

    public virtual async Task<TModel> FirstOrDefaultAsync<TModel>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        where TModel : BaseModel
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(where, cancellationToken);

        return (TModel)Activator.CreateInstance<TModel>().FromEntity(entity);
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
        return await _context.Set<TEntity>().Where(where).OrderByDescending(a => a.DataCriacao).PaginationAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<PagedResult<TModel>> ToListAsync<TModel>(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        where TModel : BaseModel
    {
        return await _context.Set<TEntity>().Where(where).OrderByDescending(a => a.DataCriacao).PaginationAsync<TEntity, TModel>(pageNumber, pageSize, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> ToListTrackingAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(where).AsTracking().ToListAsync(cancellationToken);
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
