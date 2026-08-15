using DocsViewer.Domain.Categories;

namespace DocsViewer.Application.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Category category, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
