using DocsViewer.Domain.Documents;

namespace DocsViewer.Application.Documents;

/// <summary>
/// Caso de uso de adicionar uma DocumentRevision a um Document existente. O DocsViewer não
/// cria, aprova ou infere revisões (BR-REV-002/003) — o identificador é sempre informado
/// livremente, refletindo o processo de controle documental da organização.
/// </summary>
public sealed class DocumentRevisionService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentRevisionService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<DocumentRevision> AddRevisionAsync(
        Guid documentId,
        string revisionIdentifier,
        CancellationToken cancellationToken = default)
    {
        var document = await _documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new InvalidOperationException($"Documento '{documentId}' não encontrado.");

        var revision = new DocumentRevision(Guid.NewGuid(), documentId, revisionIdentifier);

        document.AddRevision(revision);

        await _documentRepository.SaveChangesAsync(cancellationToken);

        return revision;
    }
}
