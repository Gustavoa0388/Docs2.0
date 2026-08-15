using DocsViewer.Domain.Categories;
using DocsViewer.Domain.DocumentTypes;
using DocsViewer.Domain.Documents;
using DocsViewer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DocsViewer.IntegrationTests.Persistence;

/// <summary>
/// Testa o modelo EF Core (metadados de mapeamento: chaves, FKs, nullability, delete
/// behavior) construído pelo provider real do SQL Server, sem exigir conexão/banco
/// disponível — nenhuma conexão é aberta nestes testes, apenas o build do modelo é
/// inspecionado. Não usa um provider diferente (ex.: InMemory/SQLite), para não dar
/// falsa confiança sobre comportamento específico do SQL Server.
/// </summary>
public class DocsViewerDbContextModelTests
{
    private static DocsViewerDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DocsViewerDbContext>()
            .UseSqlServer("Server=localhost;Database=DocsViewerOmniModelTest;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        return new DocsViewerDbContext(options);
    }

    [Fact]
    public void Modelo_Registra_Todos_Os_DbSets_Do_Dominio_Documental()
    {
        using var context = CreateContext();

        Assert.NotNull(context.Model.FindEntityType(typeof(Document)));
        Assert.NotNull(context.Model.FindEntityType(typeof(DocumentRevision)));
        Assert.NotNull(context.Model.FindEntityType(typeof(OfficialFile)));
        Assert.NotNull(context.Model.FindEntityType(typeof(Category)));
        Assert.NotNull(context.Model.FindEntityType(typeof(DocumentType)));
    }

    [Fact]
    public void Document_CategoryId_E_DocumentTypeId_Sao_Opcionais_Com_Delete_Restrict()
    {
        using var context = CreateContext();
        var documentType = context.Model.FindEntityType(typeof(Document))!;

        var categoryFk = documentType.GetForeignKeys().Single(fk => fk.PrincipalEntityType.ClrType == typeof(Category));
        var documentTypeFk = documentType.GetForeignKeys().Single(fk => fk.PrincipalEntityType.ClrType == typeof(DocumentType));

        Assert.False(categoryFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, categoryFk.DeleteBehavior);

        Assert.False(documentTypeFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, documentTypeFk.DeleteBehavior);
    }

    [Fact]
    public void DocumentRevision_Pertence_A_Document_Obrigatoriamente_Com_Delete_Restrict()
    {
        using var context = CreateContext();
        var revisionType = context.Model.FindEntityType(typeof(DocumentRevision))!;

        var documentFk = revisionType.GetForeignKeys().Single();

        Assert.True(documentFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, documentFk.DeleteBehavior);
        Assert.Equal(typeof(Document), documentFk.PrincipalEntityType.ClrType);
    }

    [Fact]
    public void OfficialFile_Pertence_A_Document_Obrigatoriamente_E_A_Revision_Opcionalmente()
    {
        using var context = CreateContext();
        var officialFileType = context.Model.FindEntityType(typeof(OfficialFile))!;

        var documentFk = officialFileType.GetForeignKeys().Single(fk => fk.PrincipalEntityType.ClrType == typeof(Document));
        var revisionFk = officialFileType.GetForeignKeys().Single(fk => fk.PrincipalEntityType.ClrType == typeof(DocumentRevision));

        Assert.True(documentFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, documentFk.DeleteBehavior);

        Assert.False(revisionFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, revisionFk.DeleteBehavior);
    }

    [Fact]
    public void Category_Referencia_Pai_Opcional_Sem_Cascade()
    {
        using var context = CreateContext();
        var categoryType = context.Model.FindEntityType(typeof(Category))!;

        var parentFk = categoryType.GetForeignKeys().Single();

        Assert.False(parentFk.IsRequired);
        Assert.Equal(DeleteBehavior.Restrict, parentFk.DeleteBehavior);
        Assert.Equal(typeof(Category), parentFk.PrincipalEntityType.ClrType);
    }

    [Fact]
    public void Nenhuma_Relacao_Do_Dominio_Documental_Usa_Cascade_Delete()
    {
        using var context = CreateContext();

        var entityTypes = new[] { typeof(Document), typeof(DocumentRevision), typeof(OfficialFile), typeof(Category), typeof(DocumentType) };

        foreach (var entityType in entityTypes)
        {
            var foreignKeys = context.Model.FindEntityType(entityType)!.GetForeignKeys();
            Assert.All(foreignKeys, fk => Assert.Equal(DeleteBehavior.Restrict, fk.DeleteBehavior));
        }
    }
}
