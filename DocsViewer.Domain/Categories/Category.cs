namespace DocsViewer.Domain.Categories;

/// <summary>
/// Categoria configurável por organização, hierárquica, com pai opcional (DEC-DOM-003).
/// Sem limite de profundidade artificial.
/// </summary>
public sealed class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid? ParentCategoryId { get; private set; }

    private Category()
    {
    }

    public Category(Guid id, string name, Guid? parentCategoryId = null)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Category.Id não pode ser vazio.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category.Name é obrigatório.", nameof(name));
        }

        if (parentCategoryId == id)
        {
            throw new InvalidOperationException("Uma categoria não pode ser pai de si mesma.");
        }

        Id = id;
        Name = name;
        ParentCategoryId = parentCategoryId;
    }

    public void SetParent(Guid? parentCategoryId)
    {
        if (parentCategoryId == Id)
        {
            throw new InvalidOperationException("Uma categoria não pode ser pai de si mesma.");
        }

        ParentCategoryId = parentCategoryId;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category.Name é obrigatório.", nameof(name));
        }

        Name = name;
    }
}
