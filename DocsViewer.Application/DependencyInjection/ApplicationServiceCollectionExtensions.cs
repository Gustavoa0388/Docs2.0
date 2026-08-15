using DocsViewer.Application.Categories;
using DocsViewer.Application.DocumentTypes;
using DocsViewer.Application.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace DocsViewer.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<DocumentTypeService>();
        services.AddScoped<DocumentService>();
        services.AddScoped<DocumentRevisionService>();

        return services;
    }
}
