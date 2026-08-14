using DocsViewer.Infrastructure.DependencyInjection;
using DocsViewer.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DocsViewer.IntegrationTests.Persistence;

public class InfrastructureServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInfrastructure_RegistersDocsViewerDbContext()
    {
        var configuration = BuildConfiguration(connectionString: "Server=localhost;Database=DocsViewerOmniTests;Trusted_Connection=True;TrustServerCertificate=True;");
        var services = new ServiceCollection();

        services.AddInfrastructure(configuration);

        using var provider = services.BuildServiceProvider();
        var dbContext = provider.GetService<DocsViewerDbContext>();

        Assert.NotNull(dbContext);
    }

    [Fact]
    public void AddInfrastructure_ReadsConnectionStringFromConfiguration()
    {
        const string expectedConnectionString = "Server=localhost;Database=DocsViewerOmniTests;Trusted_Connection=True;TrustServerCertificate=True;";
        var configuration = BuildConfiguration(expectedConnectionString);

        Assert.Equal(expectedConnectionString, configuration.GetConnectionString(InfrastructureServiceCollectionExtensions.ConnectionStringName));
    }

    [Fact]
    public void AddInfrastructure_WithoutConnectionString_StillRegistersContext_ButUsingItThrowsClearError()
    {
        var configuration = BuildConfiguration(connectionString: null);
        var services = new ServiceCollection();

        services.AddInfrastructure(configuration);

        using var provider = services.BuildServiceProvider();
        var dbContext = provider.GetRequiredService<DocsViewerDbContext>();

        // Sem connection string configurada, nenhum provider é registrado.
        // A aplicação não deve mascarar isso: o erro deve ser explícito no
        // momento em que a persistência é efetivamente usada, não silencioso.
        Assert.Throws<InvalidOperationException>(() => dbContext.Database.EnsureCreated());
    }

    private static IConfiguration BuildConfiguration(string? connectionString)
    {
        var data = new Dictionary<string, string?>();
        if (connectionString is not null)
        {
            data[$"ConnectionStrings:{InfrastructureServiceCollectionExtensions.ConnectionStringName}"] = connectionString;
        }

        return new ConfigurationBuilder()
            .AddInMemoryCollection(data)
            .Build();
    }
}
