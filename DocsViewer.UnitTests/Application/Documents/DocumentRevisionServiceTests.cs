using DocsViewer.Application.Documents;
using DocsViewer.UnitTests.TestDoubles;
using Xunit;

namespace DocsViewer.UnitTests.Application.Documents;

public class DocumentRevisionServiceTests
{
    private static (DocumentService Documents, DocumentRevisionService Revisions) CreateServices()
    {
        var categoryRepository = new FakeCategoryRepository();
        var documentTypeRepository = new FakeDocumentTypeRepository();
        var documentRepository = new FakeDocumentRepository();

        var documents = new DocumentService(documentRepository, categoryRepository, documentTypeRepository);
        var revisions = new DocumentRevisionService(documentRepository);

        return (documents, revisions);
    }

    [Theory]
    [InlineData("00")]
    [InlineData("01")]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C1")]
    public async Task AddRevisionAsync_Aceita_Identificador_Nao_Numerico_Ou_Numerico_Como_Texto_Livre(string identificador)
    {
        var (documents, revisions) = CreateServices();
        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, documentTypeId: null);

        var revision = await revisions.AddRevisionAsync(document.Id, identificador);

        Assert.Equal(identificador, revision.RevisionIdentifier);
    }

    [Fact]
    public async Task AddRevisionAsync_Vincula_Revisao_Ao_Documento_Que_Passa_A_Listar_A_Revisao()
    {
        var (documents, revisions) = CreateServices();
        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, documentTypeId: null);

        await revisions.AddRevisionAsync(document.Id, "00");
        await revisions.AddRevisionAsync(document.Id, "01");

        var atualizado = await documents.GetByIdAsync(document.Id);
        Assert.Equal(2, atualizado!.Revisions.Count);
    }

    [Fact]
    public async Task AddRevisionAsync_Rejeita_Documento_Inexistente()
    {
        var (_, revisions) = CreateServices();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => revisions.AddRevisionAsync(Guid.NewGuid(), "00"));
    }

    [Fact]
    public async Task Documento_Sem_Nenhuma_Revisao_Adicionada_Permanece_Valido()
    {
        var (documents, _) = CreateServices();

        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, documentTypeId: null);

        var carregado = await documents.GetByIdAsync(document.Id);
        Assert.Empty(carregado!.Revisions);
    }
}
