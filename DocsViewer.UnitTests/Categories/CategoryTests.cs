using DocsViewer.Domain.Categories;
using Xunit;

namespace DocsViewer.UnitTests.Categories;

public class CategoryTests
{
    [Fact]
    public void Category_Pode_Ser_Raiz()
    {
        var category = new Category(Guid.NewGuid(), "Normas");

        Assert.Null(category.ParentCategoryId);
    }

    [Fact]
    public void Category_Pode_Possuir_Pai()
    {
        var parent = new Category(Guid.NewGuid(), "Normas");
        var child = new Category(Guid.NewGuid(), "Normas Internas", parent.Id);

        Assert.Equal(parent.Id, child.ParentCategoryId);
    }

    [Fact]
    public void Category_Nao_Pode_Ser_Pai_De_Si_Mesma_Na_Criacao()
    {
        var id = Guid.NewGuid();

        Assert.Throws<InvalidOperationException>(() => new Category(id, "Normas", id));
    }

    [Fact]
    public void Category_Nao_Pode_Ser_Pai_De_Si_Mesma_Ao_Reatribuir_Pai()
    {
        var category = new Category(Guid.NewGuid(), "Normas");

        Assert.Throws<InvalidOperationException>(() => category.SetParent(category.Id));
    }
}
