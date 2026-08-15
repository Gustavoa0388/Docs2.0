using DocsViewer.Domain.DocumentTypes;

namespace DocsViewer.Application.DocumentTypes;

public interface IDocumentTypeRepository
{
    Task<DocumentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DocumentType>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(DocumentType documentType, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
