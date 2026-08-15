using DocsViewer.Application.Categories;
using DocsViewer.Domain.Categories;

namespace DocsViewer.UnitTests.TestDoubles;

/// <summary>
/// Repositório em memória para testar casos de uso de Application sem depender de EF Core/SQL
/// Server. Não substitui os testes de integração do modelo EF Core (que continuam validando o
/// provider real do SQL Server) — cobre apenas a lógica de orquestração dos serviços.
/// </summary>
public sealed class FakeCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new();

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_categories.FirstOrDefault(c => c.Id == id));

    public Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Category>>(_categories.ToList());

    public Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        _categories.Add(category);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
