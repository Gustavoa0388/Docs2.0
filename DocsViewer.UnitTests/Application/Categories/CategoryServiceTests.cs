using DocsViewer.Application.Categories;
using DocsViewer.UnitTests.TestDoubles;
using Xunit;

namespace DocsViewer.UnitTests.Application.Categories;

public class CategoryServiceTests
{
    private static CategoryService CreateService(out FakeCategoryRepository repository)
    {
        repository = new FakeCategoryRepository();
        return new CategoryService(repository);
    }

    [Fact]
    public async Task CreateAsync_Cria_Categoria_Raiz_Sem_Pai()
    {
        var service = CreateService(out _);

        var category = await service.CreateAsync("Normas", parentCategoryId: null);

        Assert.Null(category.ParentCategoryId);
        Assert.Equal("Normas", category.Name);
    }

    [Fact]
    public async Task CreateAsync_Aceita_Categoria_Pai_Existente_Formando_Hierarquia()
    {
        var service = CreateService(out _);
        var raiz = await service.CreateAsync("Normas", parentCategoryId: null);

        var filha = await service.CreateAsync("Normas de Qualidade", raiz.Id);

        Assert.Equal(raiz.Id, filha.ParentCategoryId);
    }

    [Fact]
    public async Task CreateAsync_Rejeita_Categoria_Pai_Inexistente()
    {
        var service = CreateService(out _);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.CreateAsync("Normas", Guid.NewGuid()));
    }

    [Fact]
    public async Task RenameAsync_Atualiza_Nome_Da_Categoria_Existente()
    {
        var service = CreateService(out _);
        var category = await service.CreateAsync("Normas", parentCategoryId: null);

        await service.RenameAsync(category.Id, "Normas de Qualidade");

        var categorias = await service.ListAsync();
        Assert.Equal("Normas de Qualidade", categorias.Single(c => c.Id == category.Id).Name);
    }

    [Fact]
    public async Task SetParentAsync_Rejeita_Autorreferencia()
    {
        var service = CreateService(out _);
        var category = await service.CreateAsync("Normas", parentCategoryId: null);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.SetParentAsync(category.Id, category.Id));
    }

    [Fact]
    public async Task SetParentAsync_Altera_Categoria_Pai_Para_Outra_Categoria_Existente()
    {
        var service = CreateService(out _);
        var raiz1 = await service.CreateAsync("Normas", parentCategoryId: null);
        var raiz2 = await service.CreateAsync("Procedimentos", parentCategoryId: null);
        var filha = await service.CreateAsync("Subcategoria", raiz1.Id);

        await service.SetParentAsync(filha.Id, raiz2.Id);

        var categorias = await service.ListAsync();
        Assert.Equal(raiz2.Id, categorias.Single(c => c.Id == filha.Id).ParentCategoryId);
    }

    [Fact]
    public async Task ListAsync_Retorna_Vazio_Quando_Nao_Ha_Categorias()
    {
        var service = CreateService(out _);

        var categorias = await service.ListAsync();

        Assert.Empty(categorias);
    }
}
