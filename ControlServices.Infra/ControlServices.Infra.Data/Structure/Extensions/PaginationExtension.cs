using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Infra.Data.Structure.Extensions;

public static class PaginationExtension
{
    public static async Task<PagedResult<TModel>> PaginationAsync<TEntity, TModel>(this IQueryable<TEntity> elements, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where TModel : BaseModel
        where TEntity : IEntity
    {
        var count = await elements.CountAsync(cancellationToken);

        var model = Activator.CreateInstance<TModel>();

        var itens = await elements
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(entity => (TModel)model.FromEntity(entity))
            .ToListAsync(cancellationToken);

        return new PagedResult<TModel>(itens, pageNumber, pageSize, count);
    }

    public static async Task<PagedResult<T>> PaginationAsync<T>(this IQueryable<T> elements, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await elements.CountAsync(cancellationToken);

        var itens = await elements
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(itens, pageNumber, pageSize, count);
    }

    public static Task<PagedResult<T>> PaginationAsync<T>(this IEnumerable<T> elements, int pageNumber, int pageSize)
    {
        var count = elements.Count();

        var itens = elements
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(
            new PagedResult<T>(itens, pageNumber, pageSize, count));
    }
}
