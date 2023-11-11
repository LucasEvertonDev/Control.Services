using System.Linq.Expressions;
using Authentication.Application.Domain.Structure.Pagination;

namespace Authentication.Application.Domain.Structure.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity domain);

    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    Task DeleteAsync(params TEntity[] entidadesParaExcluir);

    IQueryable<TEntity> AsQueriableTracking();

    IQueryable<TEntity> AsQueriable();

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ToListAsync();

    Task<TEntity> FirstOrDefaultTrackingAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> UpdateAsync(TEntity domain);
}
