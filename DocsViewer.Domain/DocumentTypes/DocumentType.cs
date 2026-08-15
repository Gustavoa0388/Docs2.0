namespace DocsViewer.Domain.DocumentTypes;

/// <summary>
/// Tipo documental configurável por organização, independente de Category (DEC-DOM-004).
/// Nenhum valor é fixado pelo core.
/// </summary>
public sealed class DocumentType
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    private DocumentType()
    {
    }

    public DocumentType(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("DocumentType.Id não pode ser vazio.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("DocumentType.Name é obrigatório.", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("DocumentType.Name é obrigatório.", nameof(name));
        }

        Name = name;
    }
}
