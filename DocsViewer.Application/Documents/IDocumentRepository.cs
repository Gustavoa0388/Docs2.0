using DocsViewer.Domain.Documents;

namespace DocsViewer.Application.Documents;

public interface IDocumentRepository
{
    /// <summary>Carrega o Document com suas Revisions (quando existirem).</summary>
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Lista os Documents com suas Revisions (quando existirem), para catálogo/consulta.</summary>
    Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Document document, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
