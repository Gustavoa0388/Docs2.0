using DocsViewer.Application.DocumentTypes;
using DocsViewer.UnitTests.TestDoubles;
using Xunit;

namespace DocsViewer.UnitTests.Application.DocumentTypes;

public class DocumentTypeServiceTests
{
    private static DocumentTypeService CreateService()
        => new(new FakeDocumentTypeRepository());

    [Fact]
    public async Task CreateAsync_Cria_Tipo_Documental()
    {
        var service = CreateService();

        var documentType = await service.CreateAsync("Procedimento Operacional Padrão");

        Assert.Equal("Procedimento Operacional Padrão", documentType.Name);
    }

    [Fact]
    public async Task ListAsync_Retorna_Tipos_Documentais_Criados()
    {
        var service = CreateService();
        await service.CreateAsync("Norma");
        await service.CreateAsync("Instrução de Trabalho");

        var tipos = await service.ListAsync();

        Assert.Equal(2, tipos.Count);
    }

    [Fact]
    public async Task RenameAsync_Atualiza_Nome_Do_Tipo_Documental_Existente()
    {
        var service = CreateService();
        var documentType = await service.CreateAsync("Norma");

        await service.RenameAsync(documentType.Id, "Norma Técnica");

        var tipos = await service.ListAsync();
        Assert.Equal("Norma Técnica", tipos.Single(t => t.Id == documentType.Id).Name);
    }

    [Fact]
    public async Task RenameAsync_Rejeita_Tipo_Documental_Inexistente()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.RenameAsync(Guid.NewGuid(), "Norma Técnica"));
    }
}
