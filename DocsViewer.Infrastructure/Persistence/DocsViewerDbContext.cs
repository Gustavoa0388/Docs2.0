using Microsoft.EntityFrameworkCore;

namespace DocsViewer.Infrastructure.Persistence;

public sealed class DocsViewerDbContext : DbContext
{
    public DocsViewerDbContext(DbContextOptions<DocsViewerDbContext> options)
        : base(options)
    {
    }
}
