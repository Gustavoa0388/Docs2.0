using DocsViewer.Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocsViewer.Infrastructure.Persistence.Configurations;

public sealed class DocumentRevisionConfiguration : IEntityTypeConfiguration<DocumentRevision>
{
    public void Configure(EntityTypeBuilder<DocumentRevision> builder)
    {
        builder.ToTable("DocumentRevisions");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        // RevisionIdentifier não é necessariamente numérico (ex.: "00", "A", "C1") — apenas
        // texto obrigatório, sem formato/comprimento fixado por falta de fonte documental.
        builder.Property(r => r.RevisionIdentifier)
            .IsRequired();

        // Exclusão conservadora: uma Document não pode ser excluído enquanto possuir Revisões.
        builder.HasOne<Document>()
            .WithMany(d => d.Revisions)
            .HasForeignKey(r => r.DocumentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
