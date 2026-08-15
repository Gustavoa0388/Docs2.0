namespace DocsViewer.Domain.Documents;

/// <summary>
/// Revisão Documental. Só existe para Document cujo processo utilize revisão (DEC-DOM-001).
/// Não decide sua própria vigência documental — isso pertence ao processo de controle
/// documental da organização, não ao DocsViewer.
/// </summary>
public sealed class DocumentRevision
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }

    /// <summary>
    /// Identificador da revisão. Não é necessariamente numérico (ex.: "00", "01", "A", "B", "C1").
    /// Não deve ser usado para inferir ordenação/vigência (ex.: MAX(Revision) não é uma regra válida).
    /// </summary>
    public string RevisionIdentifier { get; private set; } = null!;

    private DocumentRevision()
    {
    }

    public DocumentRevision(Guid id, Guid documentId, string revisionIdentifier)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("DocumentRevision.Id não pode ser vazio.", nameof(id));
        }

        if (documentId == Guid.Empty)
        {
            throw new ArgumentException("DocumentRevision.DocumentId é obrigatório.", nameof(documentId));
        }

        if (string.IsNullOrWhiteSpace(revisionIdentifier))
        {
            throw new ArgumentException("DocumentRevision.RevisionIdentifier é obrigatório.", nameof(revisionIdentifier));
        }

        Id = id;
        DocumentId = documentId;
        RevisionIdentifier = revisionIdentifier;
    }
}
