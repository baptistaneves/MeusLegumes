using MeusLegumes.Domain.Contexts.Pedidos.Entities;
using MeusLegumes.Domain.DomainObjects;

namespace MeusLegumes.Domain.Tests.Contexts;

public class PedidoTests
{
    [Fact(DisplayName = "Adicionar Item Novo Pedido")]
    [Trait("Categoria", "Pedidos - Vendas ")]
    public void Pedido_AdicionarPedidoItem_DeveAtualizarValor()
    {
        //arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Batata", 2, 100);

        //Act
        pedido.AdicionarItem(pedidoItem);

        //Assert
        Assert.Equal(200, pedido.ValorTotal);
    }

    [Fact(DisplayName = "Adicionar item existente no pedido")]
    [Trait("Categoria", "Pedido - Pedido Item ")]
    public void AdicionarItem_ItemExistente_DeveIncrementarUnidadesSomarValores()
    {
        //arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();
        var pedidoItem = new PedidoItem(produtoId, "Batata", 2, 100);
        pedido.AdicionarItem(pedidoItem);

        var pedidoItem2 = new PedidoItem(produtoId, "Batata", 1, 100);

        //Act
        pedido.AdicionarItem(pedidoItem2);

        //Assert
        Assert.Equal(300, pedido.ValorTotal);
        Assert.Equal(1, pedido.PedidoItens.Count());
        Assert.Equal(3, pedido.PedidoItens.FirstOrDefault(i => i.ProdutoId == produtoId).Quantidade);
    }

    [Fact(DisplayName = "Remover item inexistente")]
    [Trait("Categoria", "Pedido - Vendas")]
    public void RemoverItem_ItemNaoExistenteNaLista_DeveRetornarException()
    {
        //Arrange 
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Batata", 2, 100);

        //Act & Assert
        Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));
    }

    [Fact(DisplayName = "Remover item existente")]
    [Trait("Categoria", "Pedido item")]
    public void RemoverItem_ItemExistenteNaLista_DeveAtualizarValorTotal()
    {
        //arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Batata", 2, 100);
        pedido.AdicionarItem(pedidoItem);

        var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Alface", 1, 100);
        pedido.AdicionarItem(pedidoItem2);

        //Act
        pedido.RemoverItem(pedidoItem2);

        //Assert
        Assert.Equal(200, pedido.ValorTotal);
        Assert.False(pedido.PedidoItens.Any(i => i.ProdutoId == pedidoItem2.ProdutoId));
    }

    [Fact(DisplayName = "Atualizar item inexistente")]
    [Trait("Categoria", "Pedido - Vendas")]
    public void AtualizarItem_ItemNaoExistenteNaLista_DeveRetornarException()
    {
        //Arrange 
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Batata", 2, 100);

        //Act & Assert
        Assert.Throws<DomainException>(() => pedido.ActualizarItem(pedidoItem));
    }

    [Fact(DisplayName = "Atualizar item existente")]
    [Trait("Categoria", "Pedido item")]
    public void AtualizarItem_ItemExistenteNaLista_DeveAtualizarValorTotal()
    {
        //arrange
        var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
        var produtoId = Guid.NewGuid();

        var pedidoItem = new PedidoItem(Guid.NewGuid(), "Batata", 2, 100);
        pedido.AdicionarItem(pedidoItem);

        var pedidoItem2 = new PedidoItem(produtoId, "Alface", 1, 100);
        pedido.AdicionarItem(pedidoItem2);

        var produtoAtualizado = new PedidoItem(produtoId, "Alface", 2, 100);
        //Act
        pedido.ActualizarItem(produtoAtualizado);

        //Assert
        Assert.Equal(400, pedido.ValorTotal);
    }
}
