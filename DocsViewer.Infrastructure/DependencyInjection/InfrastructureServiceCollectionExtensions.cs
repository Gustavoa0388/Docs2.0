using DocsViewer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocsViewer.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public const string ConnectionStringName = "DocsViewerDatabase";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        services.AddDbContext<DocsViewerDbContext>(options =>
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseSqlServer(connectionString);
            }
        });

        return services;
    }
}
