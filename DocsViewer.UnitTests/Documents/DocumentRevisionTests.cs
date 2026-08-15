using DocsViewer.Domain.Documents;
using Xunit;

namespace DocsViewer.UnitTests.Documents;

public class DocumentRevisionTests
{
    [Theory]
    [InlineData("00")]
    [InlineData("01")]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C1")]
    public void Identificador_De_Revisao_Nao_Precisa_Ser_Numerico(string revisionIdentifier)
    {
        var documentId = Guid.NewGuid();

        var revision = new DocumentRevision(Guid.NewGuid(), documentId, revisionIdentifier);

        Assert.Equal(revisionIdentifier, revision.RevisionIdentifier);
    }

    [Fact]
    public void DocumentRevision_Requer_DocumentId_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new DocumentRevision(Guid.NewGuid(), Guid.Empty, "00"));
    }

    [Fact]
    public void DocumentRevision_Requer_Identificador_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new DocumentRevision(Guid.NewGuid(), Guid.NewGuid(), " "));
    }
}
