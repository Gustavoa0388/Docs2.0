using DocsViewer.Domain.Categories;
using DocsViewer.Domain.DocumentTypes;
using DocsViewer.Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocsViewer.Infrastructure.Persistence.Configurations;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        // Sem limite de tamanho: nenhuma regra de formato/comprimento para o código
        // documental foi definida em documentação aprovada até esta data.
        builder.Property(d => d.Code)
            .IsRequired();

        // Título exigido por URS-UX-002 / URS-VWR-013 (DV2-URS-001 v0.3). Sem limite de
        // tamanho definido em documentação aprovada.
        builder.Property(d => d.Title)
            .IsRequired();

        // Comportamento conservador de exclusão: não é possível excluir uma Category ou
        // DocumentType em uso por um Document.
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasOne<DocumentType>()
            .WithMany()
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
