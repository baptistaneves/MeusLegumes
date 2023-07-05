using MeusLegumes.Application.Contexts.Categorias;
using System.Net.Http.Json;

namespace MeusLegumes.API.Tests.Contexts.Categorias;

[Collection(nameof(CategoriaIntegrationTestsCollection))]
public class CategoriaIntegrationTests
{
    private readonly CategoriaIntegrationTestsFixture _fixture;

    public CategoriaIntegrationTests(CategoriaIntegrationTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Adicionar categoria com sucesso")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_AdicionarCategoria_DeveRetornarComSucesso()
    {
        //Arrange
        var novaCategoria = _fixture.AdicionarNovaCategoriaValida();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/categorias/nova-categoria", novaCategoria);

        //Assert
        postResponse.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "Adicionar categoria sem com descrição vázia")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_AdicionarCategoria_ComDescricaoVaziaDeveRetornarComFalha()
    {
        //Arrange
        var novaCategoria = _fixture.AdicionarCategoriaSemDescricao();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/categorias/nova-categoria", novaCategoria);

        //Assert 
        Assert.Contains(CategoriaErrorMessages.CategoriaNaoPodeSerVazia, await postResponse.Content.ReadAsStringAsync());
        Assert.False(postResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Adicionar categoria sem o minímo de caraceteres exigido")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_AdicionarCategoria_ComCaracteresAbaixoDe4DeveRetornarComFalha()
    {
        //Arrange
        var novaCategoria = _fixture.AdicionarNovaCategoriaInValida();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/categorias/nova-categoria", novaCategoria);

        //Assert 
        Assert.Contains(CategoriaErrorMessages.CategoriaComMinimoDeCaracteres, await postResponse.Content.ReadAsStringAsync());
        Assert.False(postResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Adicionar categoria existente")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_AdicionarCategoria_CategoriaExisteDeveRetornarComFalha()
    {
        //Arrange
        var categoria = _fixture.AdicionarCategoriaExistente();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/categorias/nova-categoria", categoria);
        var messages = await postResponse.Content.ReadAsStringAsync();

        //Assert 
        Assert.Contains(CategoriaErrorMessages.CategoriaJaExiste, messages);
        Assert.False(postResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Remover categoria com sucesso")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_RemoverCategoria_DeveRetornarComSucesso()
    {
        //Arrange
        var categoriaId = new Guid("F7B7EF73-26CB-43A4-B6D8-26589E75C452");

        //Act
        var postResponse = await _fixture.Client.DeleteAsync($"api/v1/categorias/remover-categoria/{categoriaId}");

        //Assert
        postResponse.EnsureSuccessStatusCode(); 
    }

    [Fact(DisplayName = "Remover categoria com produtos cadastrados")]
    [Trait("Categoria", "Integração API - Categoria")]
    public async Task Categoria_RemoverCategoria_CategoriaPossuiProdutosCadastradosNaoPodeSerRemovida()
    {
        //Arrange
        var categoriaId = new Guid("1E4FABD9-7F41-4C1C-B0A5-DBC0AC8342F6");

        //Act
        var postResponse = await _fixture.Client.DeleteAsync($"api/v1/categorias/remover-categoria/{categoriaId}");

        //Assert
        Assert.Contains(CategoriaErrorMessages.CategoriaNaoPodeSerRemovida, await postResponse.Content.ReadAsStringAsync());
        Assert.False(postResponse.IsSuccessStatusCode);
    }
}

