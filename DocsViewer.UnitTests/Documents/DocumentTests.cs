using DocsViewer.Domain.Documents;
using Xunit;

namespace DocsViewer.UnitTests.Documents;

public class DocumentTests
{
    [Fact]
    public void Documento_Sem_Revisao_E_Valido()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");

        Assert.Empty(document.Revisions);
    }

    [Fact]
    public void Documento_Pode_Possuir_Revisao()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");
        var revision = new DocumentRevision(Guid.NewGuid(), document.Id, "00");

        document.Revisions.Add(revision);

        Assert.Single(document.Revisions);
        Assert.Same(revision, document.Revisions.Single());
    }

    [Fact]
    public void Document_Requer_Id_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new Document(Guid.Empty, "DOC-001", "Norma de Qualidade"));
    }

    [Fact]
    public void Document_Requer_Code_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new Document(Guid.NewGuid(), " ", "Norma de Qualidade"));
    }

    [Fact]
    public void Document_Requer_Title_Nao_Vazio()
    {
        Assert.Throws<ArgumentException>(() => new Document(Guid.NewGuid(), "DOC-001", " "));
    }
}
