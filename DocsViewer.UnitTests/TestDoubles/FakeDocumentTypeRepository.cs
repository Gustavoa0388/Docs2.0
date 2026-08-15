using DocsViewer.Application.DocumentTypes;
using DocsViewer.Domain.DocumentTypes;

namespace DocsViewer.UnitTests.TestDoubles;

public sealed class FakeDocumentTypeRepository : IDocumentTypeRepository
{
    private readonly List<DocumentType> _documentTypes = new();

    public Task<DocumentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_documentTypes.FirstOrDefault(t => t.Id == id));

    public Task<IReadOnlyList<DocumentType>> ListAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<DocumentType>>(_documentTypes.ToList());

    public Task AddAsync(DocumentType documentType, CancellationToken cancellationToken = default)
    {
        _documentTypes.Add(documentType);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
