using DocsViewer.Infrastructure.Persistence;
using Xunit;

namespace DocsViewer.IntegrationTests.Persistence;

public class InfrastructureLayeringTests
{
    [Fact]
    public void Infrastructure_Does_Not_Reference_Web()
    {
        var infrastructureAssembly = typeof(DocsViewerDbContext).Assembly;

        var referencesWeb = infrastructureAssembly
            .GetReferencedAssemblies()
            .Any(a => a.Name == "DocsViewer.Web");

        Assert.False(referencesWeb, "DocsViewer.Infrastructure não deve referenciar DocsViewer.Web (docs/ARCHITECTURE.md).");
    }
}
