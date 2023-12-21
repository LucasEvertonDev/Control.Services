namespace Authentication.Infra.Data.Structure.Extensions;

public static class PaginationExtension
{
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
