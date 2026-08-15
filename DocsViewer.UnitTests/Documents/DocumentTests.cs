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

    [Fact]
    public void AddRevision_Vincula_Revisao_Do_Mesmo_Document()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");
        var revision = new DocumentRevision(Guid.NewGuid(), document.Id, "A");

        document.AddRevision(revision);

        Assert.Single(document.Revisions);
        Assert.Same(revision, document.Revisions.Single());
    }

    [Fact]
    public void AddRevision_De_Outro_Document_E_Invalido()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");
        var revisionDeOutroDocumento = new DocumentRevision(Guid.NewGuid(), Guid.NewGuid(), "A");

        Assert.Throws<InvalidOperationException>(() => document.AddRevision(revisionDeOutroDocumento));
    }

    [Fact]
    public void UpdateTitle_Atualiza_Titulo()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");

        document.UpdateTitle("Norma de Qualidade — Revisada");

        Assert.Equal("Norma de Qualidade — Revisada", document.Title);
    }

    [Fact]
    public void UpdateTitle_Requer_Titulo_Nao_Vazio()
    {
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade");

        Assert.Throws<ArgumentException>(() => document.UpdateTitle(" "));
    }

    [Fact]
    public void SetCategory_E_SetDocumentType_Aceitam_Nulo()
    {
        var categoryId = Guid.NewGuid();
        var documentTypeId = Guid.NewGuid();
        var document = new Document(Guid.NewGuid(), "DOC-001", "Norma de Qualidade", categoryId, documentTypeId);

        document.SetCategory(null);
        document.SetDocumentType(null);

        Assert.Null(document.CategoryId);
        Assert.Null(document.DocumentTypeId);
    }
}
