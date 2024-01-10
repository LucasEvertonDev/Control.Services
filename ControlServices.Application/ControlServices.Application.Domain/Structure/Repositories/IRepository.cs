using System.Linq.Expressions;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;

namespace ControlServices.Application.Domain.Structure.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity domain, CancellationToken cancellationToken = default);

    Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity[] entidadesParaExcluir, CancellationToken cancellationToken = default);

    IQueryable<TEntity> AsQueriableTracking(CancellationToken cancellationToken = default);

    IQueryable<TEntity> AsQueriable(CancellationToken cancellationToken = default);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    Task<TModel> FirstOrDefaultAsync<TModel>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        where TModel : BaseModel;

    Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default);

    Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    Task<PagedResult<TModel>> ToListAsync<TModel>(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        where TModel : BaseModel;

    Task<TEntity> FirstOrDefaultTrackingAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity domain, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> ToListTrackingAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
}
