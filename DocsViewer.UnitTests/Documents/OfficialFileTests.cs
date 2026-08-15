using DocsViewer.Domain.Documents;
using Xunit;

namespace DocsViewer.UnitTests.Documents;

public class OfficialFileTests
{
    private static OfficialFile CreateOfficialFile(Guid documentId, DocumentRevision? revision = null) =>
        new(
            Guid.NewGuid(),
            documentId,
            "norma-001.pdf",
            "application/pdf",
            sizeInBytes: 1024,
            hashValue: "abc123",
            hashAlgorithm: "SHA-256",
            incorporatedAtUtc: DateTime.UtcNow,
            documentRevision: revision);

    [Fact]
    public void OfficialFile_Pode_Pertencer_A_Document_Sem_Revisao()
    {
        var documentId = Guid.NewGuid();

        var officialFile = CreateOfficialFile(documentId);

        Assert.Equal(documentId, officialFile.DocumentId);
        Assert.Null(officialFile.DocumentRevisionId);
    }

    [Fact]
    public void OfficialFile_Com_Revisao_Do_Mesmo_Document_E_Valido()
    {
        var documentId = Guid.NewGuid();
        var revision = new DocumentRevision(Guid.NewGuid(), documentId, "00");

        var officialFile = CreateOfficialFile(documentId, revision);

        Assert.Equal(revision.Id, officialFile.DocumentRevisionId);
    }

    [Fact]
    public void OfficialFile_Com_Revisao_De_Outro_Document_E_Invalido()
    {
        var documentId = Guid.NewGuid();
        var otherDocumentId = Guid.NewGuid();
        var revisionDeOutroDocumento = new DocumentRevision(Guid.NewGuid(), otherDocumentId, "00");

        Assert.Throws<InvalidOperationException>(() => CreateOfficialFile(documentId, revisionDeOutroDocumento));
    }

    [Fact]
    public void OfficialFile_Requer_Hash_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new OfficialFile(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "norma-001.pdf",
            "application/pdf",
            sizeInBytes: 1024,
            hashValue: " ",
            hashAlgorithm: "SHA-256",
            incorporatedAtUtc: DateTime.UtcNow));
    }

    [Fact]
    public void OfficialFile_Nao_Aceita_Tamanho_Negativo()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new OfficialFile(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "norma-001.pdf",
            "application/pdf",
            sizeInBytes: -1,
            hashValue: "abc123",
            hashAlgorithm: "SHA-256",
            incorporatedAtUtc: DateTime.UtcNow));
    }
}
