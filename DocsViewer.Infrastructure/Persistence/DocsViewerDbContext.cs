using DocsViewer.Domain.Categories;
using DocsViewer.Domain.DocumentTypes;
using DocsViewer.Domain.Documents;
using Microsoft.EntityFrameworkCore;

namespace DocsViewer.Infrastructure.Persistence;

public sealed class DocsViewerDbContext : DbContext
{
    public DocsViewerDbContext(DbContextOptions<DocsViewerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents => Set<Document>();

    public DbSet<DocumentRevision> DocumentRevisions => Set<DocumentRevision>();

    public DbSet<OfficialFile> OfficialFiles => Set<OfficialFile>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocsViewerDbContext).Assembly);
    }
}
