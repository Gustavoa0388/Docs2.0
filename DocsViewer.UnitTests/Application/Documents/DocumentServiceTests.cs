using DocsViewer.Application.Categories;
using DocsViewer.Application.DocumentTypes;
using DocsViewer.Application.Documents;
using DocsViewer.UnitTests.TestDoubles;
using Xunit;

namespace DocsViewer.UnitTests.Application.Documents;

public class DocumentServiceTests
{
    private static (DocumentService Documents, CategoryService Categories, DocumentTypeService DocumentTypes) CreateServices()
    {
        var categoryRepository = new FakeCategoryRepository();
        var documentTypeRepository = new FakeDocumentTypeRepository();
        var documentRepository = new FakeDocumentRepository();

        var categories = new CategoryService(categoryRepository);
        var documentTypes = new DocumentTypeService(documentTypeRepository);
        var documents = new DocumentService(documentRepository, categoryRepository, documentTypeRepository);

        return (documents, categories, documentTypes);
    }

    [Fact]
    public async Task CreateAsync_Cria_Documento_Sem_Categoria_Nem_Tipo()
    {
        var (documents, _, _) = CreateServices();

        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, documentTypeId: null);

        Assert.Equal("DOC-001", document.Code);
        Assert.Null(document.CategoryId);
        Assert.Null(document.DocumentTypeId);
        Assert.Empty(document.Revisions);
    }

    [Fact]
    public async Task CreateAsync_Aceita_Categoria_E_Tipo_Documental_Existentes()
    {
        var (documents, categories, documentTypes) = CreateServices();
        var category = await categories.CreateAsync("Normas", parentCategoryId: null);
        var documentType = await documentTypes.CreateAsync("Norma Técnica");

        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", category.Id, documentType.Id);

        Assert.Equal(category.Id, document.CategoryId);
        Assert.Equal(documentType.Id, document.DocumentTypeId);
    }

    [Fact]
    public async Task CreateAsync_Rejeita_Categoria_Inexistente()
    {
        var (documents, _, _) = CreateServices();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => documents.CreateAsync("DOC-001", "Norma de Qualidade", Guid.NewGuid(), documentTypeId: null));
    }

    [Fact]
    public async Task CreateAsync_Rejeita_Tipo_Documental_Inexistente()
    {
        var (documents, _, _) = CreateServices();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, Guid.NewGuid()));
    }

    [Fact]
    public async Task UpdateAsync_Atualiza_Titulo_Categoria_E_Tipo()
    {
        var (documents, categories, documentTypes) = CreateServices();
        var document = await documents.CreateAsync("DOC-001", "Norma de Qualidade", categoryId: null, documentTypeId: null);
        var category = await categories.CreateAsync("Normas", parentCategoryId: null);
        var documentType = await documentTypes.CreateAsync("Norma Técnica");

        await documents.UpdateAsync(document.Id, "Norma de Qualidade — Revisada", category.Id, documentType.Id);

        var atualizado = await documents.GetByIdAsync(document.Id);
        Assert.Equal("Norma de Qualidade — Revisada", atualizado!.Title);
        Assert.Equal(category.Id, atualizado.CategoryId);
        Assert.Equal(documentType.Id, atualizado.DocumentTypeId);
    }

    [Fact]
    public async Task UpdateAsync_Rejeita_Documento_Inexistente()
    {
        var (documents, _, _) = CreateServices();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => documents.UpdateAsync(Guid.NewGuid(), "Título", categoryId: null, documentTypeId: null));
    }

    [Fact]
    public async Task ListAsync_Retorna_Documentos_Criados()
    {
        var (documents, _, _) = CreateServices();
        await documents.CreateAsync("DOC-001", "Norma A", categoryId: null, documentTypeId: null);
        await documents.CreateAsync("DOC-002", "Norma B", categoryId: null, documentTypeId: null);

        var lista = await documents.ListAsync();

        Assert.Equal(2, lista.Count);
    }
}
