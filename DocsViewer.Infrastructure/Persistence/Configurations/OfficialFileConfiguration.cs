using DocsViewer.Domain.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocsViewer.Infrastructure.Persistence.Configurations;

public sealed class OfficialFileConfiguration : IEntityTypeConfiguration<OfficialFile>
{
    public void Configure(EntityTypeBuilder<OfficialFile> builder)
    {
        builder.ToTable("OfficialFiles");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.FileName).IsRequired();
        builder.Property(f => f.MimeType).IsRequired();
        builder.Property(f => f.SizeInBytes).IsRequired();
        builder.Property(f => f.HashValue).IsRequired();
        builder.Property(f => f.HashAlgorithm).IsRequired();
        builder.Property(f => f.IncorporatedAtUtc).IsRequired();

        // Exclusão conservadora: uma Document não pode ser excluído enquanto possuir
        // Arquivos Oficiais associados.
        builder.HasOne<Document>()
            .WithMany(d => d.OfficialFiles)
            .HasForeignKey(f => f.DocumentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Opcional: OfficialFile pode existir sem DocumentRevision (Document pode não ter
        // revisão — DEC-DOM-001). Quando informada, a consistência Document<->Revision já é
        // garantida pelo próprio construtor da entidade (invariante de domínio).
        builder.HasOne<DocumentRevision>()
            .WithMany()
            .HasForeignKey(f => f.DocumentRevisionId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
