namespace DocsViewer.Domain.Documents;

/// <summary>
/// Identidade lógica persistente do documento. Pode existir sem nenhuma DocumentRevision
/// (DEC-DOM-001) — o processo de alguns documentos/registros não possui controle de revisão
/// (ex.: Ordens de Produção, Cartões de Não Conformidade, Certificados da Qualidade). Nenhuma
/// revisão fictícia (Rev.00, N/A, Sem revisão) é criada para satisfazer estrutura de banco.
/// </summary>
public sealed class Document
{
    /// <summary>
    /// Identificador técnico. Não carrega significado de negócio — ver <see cref="Code"/>.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Código documental (identificador de negócio), distinto de <see cref="Id"/>.
    /// </summary>
    public string Code { get; private set; } = null!;

    /// <summary>
    /// Título/nome do documento. Exigido por URS-UX-002 e URS-VWR-013 (DV2-URS-001 v0.3):
    /// "Código, título, revisão e condição documental relevante devem possuir apresentação
    /// clara"/"ser identificáveis" durante consulta e visualização.
    /// </summary>
    public string Title { get; private set; } = null!;

    public Guid? CategoryId { get; private set; }

    public Guid? DocumentTypeId { get; private set; }

    public ICollection<DocumentRevision> Revisions { get; private set; } = new List<DocumentRevision>();

    public ICollection<OfficialFile> OfficialFiles { get; private set; } = new List<OfficialFile>();

    private Document()
    {
    }

    public Document(Guid id, string code, string title, Guid? categoryId = null, Guid? documentTypeId = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Document.Id não pode ser vazio.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Document.Code é obrigatório.", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Document.Title é obrigatório.", nameof(title));
        }

        Id = id;
        Code = code;
        Title = title;
        CategoryId = categoryId;
        DocumentTypeId = documentTypeId;
    }

    /// <summary>
    /// Associa uma DocumentRevision a este Document. Reforça a mesma invariante de vínculo
    /// aplicada em <see cref="OfficialFile"/>: a revisão deve pertencer a este Document.
    /// </summary>
    public void AddRevision(DocumentRevision revision)
    {
        ArgumentNullException.ThrowIfNull(revision);

        if (revision.DocumentId != Id)
        {
            throw new InvalidOperationException(
                "DocumentRevision não pode ser adicionada a um Document diferente do seu DocumentId.");
        }

        Revisions.Add(revision);
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Document.Title é obrigatório.", nameof(title));
        }

        Title = title;
    }

    public void SetCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
    }

    public void SetDocumentType(Guid? documentTypeId)
    {
        DocumentTypeId = documentTypeId;
    }
}
