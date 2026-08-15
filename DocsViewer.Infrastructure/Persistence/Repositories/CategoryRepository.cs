using DocsViewer.Application.Categories;
using DocsViewer.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace DocsViewer.Infrastructure.Persistence.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly DocsViewerDbContext _dbContext;

    public CategoryRepository(DocsViewerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Categories.OrderBy(c => c.Name).ToListAsync(cancellationToken);

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        => await _dbContext.Categories.AddAsync(category, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
