using DocsViewer.Application.Documents;
using DocsViewer.Domain.Documents;
using Microsoft.EntityFrameworkCore;

namespace DocsViewer.Infrastructure.Persistence.Repositories;

public sealed class DocumentRepository : IDocumentRepository
{
    private readonly DocsViewerDbContext _dbContext;

    public DocumentRepository(DocsViewerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbContext.Documents
            .Include(d => d.Revisions)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Documents
            .Include(d => d.Revisions)
            .OrderBy(d => d.Code)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Document document, CancellationToken cancellationToken = default)
        => await _dbContext.Documents.AddAsync(document, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
