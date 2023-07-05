using MeusLegumes.Application.Contexts.Pedidos.Commands;
using MeusLegumes.Domain.Contexts.Pedidos.Entities;
using MeusLegumes.Domain.Contexts.Pedidos.Repositories;
using Moq;

namespace MeusLegumes.Application.Tests.Contexts.Pedidos;

[Collection(nameof(PedidoItemTestsCollection))]
public class PedidoItemTests
{
    public PedidoItemTestsFixture _fixture;
    public Pedido _pedido;
    public Guid _cliente;
    public Guid _produto;

    public PedidoItemTests(PedidoItemTestsFixture fixture)
    {
        _fixture = fixture;
        _fixture.SetUpHandlers();

        _cliente = Guid.NewGuid();
        _produto = Guid.NewGuid();
        _pedido = Pedido.PedidoFactory.NovoPedidoRascunho(_cliente);
    }

    [Fact(DisplayName = "Adicionar item do pedido com sucesso")]
    [Trait("Categoria", "Pedido - Item Pedido")] 
    public async Task AdicionarItem_AdicionarItemValido_DeveExecutarComSucesso()
    {
        //Arrange
        var command = _fixture.ObterCommandValido();

        //Act
        await _fixture.AdicionarItemHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(command.IsValid());
        _fixture.Mocker.GetMock<IPedidoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar item do pedido rascunho com sucesso")]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task AdicionarItem_ItemNoPedidoRascunho_DeveExecutarComSucesso()
    {
        //Arrange
        var command = new AdicionarPedidoItemCommand(_cliente, _produto, "Batata", 4, 200);

        _fixture.Mocker.GetMock<IPedidoRepository>().Setup(p => p.ObterPedidoRascunhoPorClienteId(_cliente)).Returns(Task.FromResult(_pedido));

        //Act
        await _fixture.AdicionarItemHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(command.IsValid());
        _fixture.Mocker.GetMock<IPedidoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

        [Fact(DisplayName = "Adicionar item do pedido com falha")]
    [Trait("Categoria", "Pedido - Item Pedido")]
    public async Task AdicionarItem_AdicionarItemInValido_DeveExecutarComFalha()
    {
        //Arrange
        var command = _fixture.ObterCommandInvalido();

        //Act
        await _fixture.AdicionarItemHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.False(command.IsValid());
        _fixture.Mocker.GetMock<IPedidoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Atualizar item do pedido rascunho com sucesso")]
    [Trait("Categoria", "Pedido - item do pedido")]
    public async Task AtualizarItem_ItemNoPedidoRascunho_DeveExecutarComSucesso()
    {
        //Arrange
        var pedidoItem = new PedidoItem(_produto, "Banana", 2, 100);
        _pedido.AdicionarItem(pedidoItem);

        var command = new AtualizarPedidoItemCommand(_cliente, _produto, 4);

        _fixture.Mocker.GetMock<IPedidoRepository>().Setup(p => p.ObterPedidoRascunhoPorClienteId(_cliente)).Returns(Task.FromResult(_pedido));
        _fixture.Mocker.GetMock<IPedidoRepository>().Setup(i => i.ObterItemPorPedido(_pedido.Id, _produto)).Returns(Task.FromResult(pedidoItem));

        //Act
        await _fixture.AtualizarItemHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(command.IsValid());
        Assert.False(command.ValidationResult.Errors.Any());
        Assert.Equal(4, _pedido.PedidoItens.FirstOrDefault(i => i.Id == pedidoItem.Id).Quantidade);
        Assert.Equal(400, _pedido.ValorTotal);
        _fixture.Mocker.GetMock<IPedidoRepository>().Verify(v => v.UnitOfWork.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Atualizar item do pedido com falha")]
    [Trait("Categoria", "Pedido - item do pedido")]
    public void AtualizarItemCommand_AtualizarItemInValido_DeveExecutarComFalha()
    {
        //Arrange
        var command = _fixture.ObterAtualizarCommandInValido();

        //Act & Assert
        Assert.False(command.IsValid());
        Assert.True(command.ValidationResult.Errors.Any());
    }
}
