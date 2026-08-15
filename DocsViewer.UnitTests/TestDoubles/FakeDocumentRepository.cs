using DocsViewer.Application.Documents;
using DocsViewer.Domain.Documents;

namespace DocsViewer.UnitTests.TestDoubles;

public sealed class FakeDocumentRepository : IDocumentRepository
{
    private readonly List<Document> _documents = new();

    public Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_documents.FirstOrDefault(d => d.Id == id));

    public Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Document>>(_documents.ToList());

    public Task AddAsync(Document document, CancellationToken cancellationToken = default)
    {
        _documents.Add(document);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
