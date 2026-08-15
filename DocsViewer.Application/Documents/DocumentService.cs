using DocsViewer.Application.Categories;
using DocsViewer.Application.DocumentTypes;
using DocsViewer.Domain.Documents;

namespace DocsViewer.Application.Documents;

/// <summary>
/// Casos de uso de Document: criar, listar, consultar e editar metadados suportados
/// (título, categoria, tipo documental). Um Document sem nenhuma DocumentRevision é um
/// estado válido (DEC-DOM-001) — nenhuma revisão é criada automaticamente aqui.
/// </summary>
public sealed class DocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDocumentTypeRepository _documentTypeRepository;

    public DocumentService(
        IDocumentRepository documentRepository,
        ICategoryRepository categoryRepository,
        IDocumentTypeRepository documentTypeRepository)
    {
        _documentRepository = documentRepository;
        _categoryRepository = categoryRepository;
        _documentTypeRepository = documentTypeRepository;
    }

    public async Task<Document> CreateAsync(
        string code,
        string title,
        Guid? categoryId,
        Guid? documentTypeId,
        CancellationToken cancellationToken = default)
    {
        await EnsureCategoryExistsAsync(categoryId, cancellationToken);
        await EnsureDocumentTypeExistsAsync(documentTypeId, cancellationToken);

        var document = new Document(Guid.NewGuid(), code, title, categoryId, documentTypeId);

        await _documentRepository.AddAsync(document, cancellationToken);
        await _documentRepository.SaveChangesAsync(cancellationToken);

        return document;
    }

    public Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default)
        => _documentRepository.ListAsync(cancellationToken);

    public Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _documentRepository.GetByIdAsync(id, cancellationToken);

    public async Task UpdateAsync(
        Guid id,
        string title,
        Guid? categoryId,
        Guid? documentTypeId,
        CancellationToken cancellationToken = default)
    {
        var document = await _documentRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException($"Documento '{id}' não encontrado.");

        await EnsureCategoryExistsAsync(categoryId, cancellationToken);
        await EnsureDocumentTypeExistsAsync(documentTypeId, cancellationToken);

        document.UpdateTitle(title);
        document.SetCategory(categoryId);
        document.SetDocumentType(documentTypeId);

        await _documentRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureCategoryExistsAsync(Guid? categoryId, CancellationToken cancellationToken)
    {
        if (categoryId is null)
        {
            return;
        }

        var category = await _categoryRepository.GetByIdAsync(categoryId.Value, cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException($"Categoria '{categoryId}' não existe.");
        }
    }

    private async Task EnsureDocumentTypeExistsAsync(Guid? documentTypeId, CancellationToken cancellationToken)
    {
        if (documentTypeId is null)
        {
            return;
        }

        var documentType = await _documentTypeRepository.GetByIdAsync(documentTypeId.Value, cancellationToken);

        if (documentType is null)
        {
            throw new InvalidOperationException($"Tipo documental '{documentTypeId}' não existe.");
        }
    }
}
