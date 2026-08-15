using DocsViewer.Application.DocumentTypes;
using DocsViewer.Domain.DocumentTypes;
using Microsoft.EntityFrameworkCore;

namespace DocsViewer.Infrastructure.Persistence.Repositories;

public sealed class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly DocsViewerDbContext _dbContext;

    public DocumentTypeRepository(DocsViewerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<DocumentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbContext.DocumentTypes.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<DocumentType>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbContext.DocumentTypes.OrderBy(t => t.Name).ToListAsync(cancellationToken);

    public async Task AddAsync(DocumentType documentType, CancellationToken cancellationToken = default)
        => await _dbContext.DocumentTypes.AddAsync(documentType, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
