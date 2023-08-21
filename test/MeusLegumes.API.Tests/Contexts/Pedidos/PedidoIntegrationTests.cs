using MeusLegumes.API.Tests.Config;
using System.Net.Http.Json;

namespace MeusLegumes.API.Tests.Contexts.Pedidos;

[Collection(nameof(PedidoIntegrationColletion))]
[TestCaseOrderer("MeusLegumes.API.Tests.Config.PriorityOrderer", "MeusLegumes.API.Tests.Config")]
public class PedidoIntegrationTests
{
    private readonly PedidoIntegrationFixture _fixture;

    public PedidoIntegrationTests(PedidoIntegrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Adicionar item do pedido com sucesso"), TestPriority(1)]
    [Trait("Categoria", "Pedido - Item Pedido")]
    public async Task AdicionarItem_AdicionarItemValido_DeveExecutarComSucesso()
    {
        //Arrange
        var item = _fixture.AdicionarNovoItemValido();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/carrinho/adicionar-item-no-carrinho", item);

        //Assert
        postResponse.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "Adicionar item do pedido rascunho com sucesso"), TestPriority(2)]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task AdicionarItem_ItemNoPedidoRascunho_DeveExecutarComSucesso()
    {
        //Arrange
        var item = _fixture.AdicionarNovoItemValidoNoPedidoRascunho();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("api/v1/carrinho/adicionar-item-no-carrinho", item);

        //Assert
        postResponse.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "Adicionar item do pedido com falha"), TestPriority(5)]
    [Trait("Categoria", "Pedido - Item Pedido")]
    public async Task AdicionarItem_AdicionarItemInValido_DeveExecutarComFalha()
    {
        //Arrange
        var item = _fixture.AdicionarNovoItemInValido();

        //Act
        var postResponse = await _fixture.Client.PostAsJsonAsync("capi/v1/arrinho/adicionar-item-no-carrinho", item);

        //Assert
        Assert.False(postResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Atualizar item do pedido rascunho com sucesso"), TestPriority(3)]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task AtualizarItem_ItemNoPedidoRascunho_DeveExecutarComSucesso()
    {
        //Arrange
        var item = _fixture.AtualizarItemValido();

        //Act
        var postResponse = await _fixture.Client.PutAsJsonAsync("api/v1/carrinho/actualizar-item-no-carrinho", item);

        //Assert
        postResponse.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "Atualizar item do pedido com falha"), TestPriority(6)]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task AtualizarItemCommand_AtualizarItemInValido_DeveExecutarComFalha()
    {
        //Arrange
        var item = _fixture.AtualizarItemInValido();

        //Act
        var postResponse = await _fixture.Client.PutAsJsonAsync("api/v1/carrinho/actualizar-item-no-carrinho", item);

        //Assert
        Assert.False(postResponse.IsSuccessStatusCode);
    }

    [Fact(DisplayName = "Remover item do pedido com sucesso"), TestPriority(4)]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task RemoverItemCommand_RemoverIte_DeveExecutarComSucesso()
    {
        //Arrange
        var produtoId = _fixture.RemoverItemValido();

        //Act
        var postResponse = await _fixture.Client.DeleteAsync($"api/v1/carrinho/remover-item-no-carrinho/{produtoId}");

        //Assert
        postResponse.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "Remover item do pedido com falha"), TestPriority(7)]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task RemoverItemCommand_RemoverIte_DeveExecutarComFalha()
    {
        //Arrange
        var produtoId = _fixture.RemoverItemInValido();

        //Act
        var postResponse = await _fixture.Client.DeleteAsync($"api/v1/carrinho/remover-item-no-carrinho/{produtoId}");

        //Assert
        Assert.False(postResponse.IsSuccessStatusCode);
    }
}
