using DocsViewer.Domain.Categories;

namespace DocsViewer.Application.Categories;

/// <summary>
/// Casos de uso de Category: criar, listar, renomear e definir/alterar categoria pai.
/// A prevenção de autorreferência é garantida pelo próprio domínio (<see cref="Category"/>).
/// </summary>
public sealed class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(string name, Guid? parentCategoryId, CancellationToken cancellationToken = default)
    {
        if (parentCategoryId is not null)
        {
            await EnsureParentExistsAsync(parentCategoryId.Value, cancellationToken);
        }

        var category = new Category(Guid.NewGuid(), name, parentCategoryId);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return category;
    }

    public Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default)
        => _categoryRepository.ListAsync(cancellationToken);

    public async Task RenameAsync(Guid id, string newName, CancellationToken cancellationToken = default)
    {
        var category = await GetExistingAsync(id, cancellationToken);

        category.Rename(newName);

        await _categoryRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task SetParentAsync(Guid id, Guid? newParentCategoryId, CancellationToken cancellationToken = default)
    {
        var category = await GetExistingAsync(id, cancellationToken);

        if (newParentCategoryId is not null)
        {
            await EnsureParentExistsAsync(newParentCategoryId.Value, cancellationToken);
        }

        category.SetParent(newParentCategoryId);

        await _categoryRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<Category> GetExistingAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException($"Categoria '{id}' não encontrada.");
        }

        return category;
    }

    private async Task EnsureParentExistsAsync(Guid parentCategoryId, CancellationToken cancellationToken)
    {
        var parent = await _categoryRepository.GetByIdAsync(parentCategoryId, cancellationToken);

        if (parent is null)
        {
            throw new InvalidOperationException($"Categoria pai '{parentCategoryId}' não existe.");
        }
    }
}
