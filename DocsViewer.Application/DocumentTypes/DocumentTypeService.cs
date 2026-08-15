using DocsViewer.Domain.DocumentTypes;

namespace DocsViewer.Application.DocumentTypes;

/// <summary>
/// Casos de uso de DocumentType: criar, listar e renomear. Nenhum tipo é fixado pelo core
/// (BR-CFG-001) — todo tipo documental é criado por configuração da implantação.
/// </summary>
public sealed class DocumentTypeService
{
    private readonly IDocumentTypeRepository _documentTypeRepository;

    public DocumentTypeService(IDocumentTypeRepository documentTypeRepository)
    {
        _documentTypeRepository = documentTypeRepository;
    }

    public async Task<DocumentType> CreateAsync(string name, CancellationToken cancellationToken = default)
    {
        var documentType = new DocumentType(Guid.NewGuid(), name);

        await _documentTypeRepository.AddAsync(documentType, cancellationToken);
        await _documentTypeRepository.SaveChangesAsync(cancellationToken);

        return documentType;
    }

    public Task<IReadOnlyList<DocumentType>> ListAsync(CancellationToken cancellationToken = default)
        => _documentTypeRepository.ListAsync(cancellationToken);

    public async Task RenameAsync(Guid id, string newName, CancellationToken cancellationToken = default)
    {
        var documentType = await _documentTypeRepository.GetByIdAsync(id, cancellationToken);

        if (documentType is null)
        {
            throw new InvalidOperationException($"Tipo documental '{id}' não encontrado.");
        }

        documentType.Rename(newName);

        await _documentTypeRepository.SaveChangesAsync(cancellationToken);
    }
}
