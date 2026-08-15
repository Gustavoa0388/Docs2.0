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

    public Guid? CategoryId { get; private set; }

    public Guid? DocumentTypeId { get; private set; }

    public ICollection<DocumentRevision> Revisions { get; private set; } = new List<DocumentRevision>();

    public ICollection<OfficialFile> OfficialFiles { get; private set; } = new List<OfficialFile>();

    private Document()
    {
    }

    public Document(Guid id, string code, Guid? categoryId = null, Guid? documentTypeId = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Document.Id não pode ser vazio.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Document.Code é obrigatório.", nameof(code));
        }

        Id = id;
        Code = code;
        CategoryId = categoryId;
        DocumentTypeId = documentTypeId;
    }
}
