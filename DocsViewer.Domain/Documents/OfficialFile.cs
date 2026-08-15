namespace DocsViewer.Domain.Documents;

/// <summary>
/// Representa a representação digital controlada (Arquivo Oficial) de um Document.
/// Pode existir sem DocumentRevision, pois um Document pode não possuir Revisão (DEC-DOM-001).
/// Quando associado a uma DocumentRevision, ela deve pertencer ao mesmo Document (invariante).
/// O hash de integridade pertence individualmente ao Arquivo Oficial e é calculado na
/// incorporação/substituição — não há requisito de recálculo em toda visualização (DEC-DOM-002).
/// Não armazena o conteúdo binário do arquivo (sem BLOB nesta tarefa).
/// </summary>
public sealed class OfficialFile
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }
    public Guid? DocumentRevisionId { get; private set; }
    public string FileName { get; private set; } = null!;
    public string MimeType { get; private set; } = null!;
    public long SizeInBytes { get; private set; }
    public string HashValue { get; private set; } = null!;
    public string HashAlgorithm { get; private set; } = null!;
    public DateTime IncorporatedAtUtc { get; private set; }

    private OfficialFile()
    {
    }

    public OfficialFile(
        Guid id,
        Guid documentId,
        string fileName,
        string mimeType,
        long sizeInBytes,
        string hashValue,
        string hashAlgorithm,
        DateTime incorporatedAtUtc,
        DocumentRevision? documentRevision = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("OfficialFile.Id não pode ser vazio.", nameof(id));
        }

        if (documentId == Guid.Empty)
        {
            throw new ArgumentException("OfficialFile.DocumentId é obrigatório.", nameof(documentId));
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("OfficialFile.FileName é obrigatório.", nameof(fileName));
        }

        if (string.IsNullOrWhiteSpace(mimeType))
        {
            throw new ArgumentException("OfficialFile.MimeType é obrigatório.", nameof(mimeType));
        }

        if (sizeInBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeInBytes), "OfficialFile.SizeInBytes não pode ser negativo.");
        }

        if (string.IsNullOrWhiteSpace(hashValue))
        {
            throw new ArgumentException("OfficialFile.HashValue é obrigatório.", nameof(hashValue));
        }

        if (string.IsNullOrWhiteSpace(hashAlgorithm))
        {
            throw new ArgumentException("OfficialFile.HashAlgorithm é obrigatório.", nameof(hashAlgorithm));
        }

        if (documentRevision is not null && documentRevision.DocumentId != documentId)
        {
            throw new InvalidOperationException(
                "OfficialFile não pode ser associado a uma DocumentRevision de um Document diferente.");
        }

        Id = id;
        DocumentId = documentId;
        DocumentRevisionId = documentRevision?.Id;
        FileName = fileName;
        MimeType = mimeType;
        SizeInBytes = sizeInBytes;
        HashValue = hashValue;
        HashAlgorithm = hashAlgorithm;
        IncorporatedAtUtc = incorporatedAtUtc;
    }
}
